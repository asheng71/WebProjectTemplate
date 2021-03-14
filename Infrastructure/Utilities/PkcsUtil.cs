using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infrastructure.Utilities
{
    public class PkcsUtil : IPkcsUtil
    {        
        private string _chainPath;
        private string _storePath;
        private string _storePW;
        //private X509Certificate2Collection _trustChain;

        public PkcsUtil(IConfiguration config)
        {   
            _storePath = config.GetValue<string>("SigingKeyStore");            
            _storePW = config.GetValue<string>("SigingKeyStorePW");
            //_chainPath = config.GetValue<string>("GcaCertChainPath");
            //_trustChain = GetGcaTrustChain();
        }

        public string Sign(string data)
        {
            return Sign(data, GetSigningKeystore());
        }

        public bool Verify(string data, string base64Signature)
        {
            return Verify(data, base64Signature, GetSigningKeystore());
        }

        /// <summary>
        /// Check if the given certificate is used for server authentication
        /// </summary>
        /// <param name="cert"></param>
        /// <returns></returns>
        public bool IsSSLServerCertificate(X509Certificate2 cert)
        {
            bool result = false;

            X509ExtensionCollection extensions = cert.Extensions;
            foreach (X509Extension ext in extensions)
            {
                if (ext is X509EnhancedKeyUsageExtension)
                {
                    var enhanced = ext as X509EnhancedKeyUsageExtension;
                    foreach (Oid oid in enhanced.EnhancedKeyUsages)
                    {
                        result = (oid.Value == "1.3.6.1.5.5.7.3.1");  // http://oidref.com/1.3.6.1.5.5.7.3.1
                        if (result)
                        {
                            return result;
                        }
                    }
                }
            }

            return result;
        }


        public X509Certificate2 GetRootCACertificate(X509Certificate2 cert)
        {
            if (cert == null)
            {
                return null;
            }

            var chain = new X509Chain();
            if (!chain.Build(cert))
            {
                return null;
            }

            X509ChainElement chainElement = chain.ChainElements[chain.ChainElements.Count - 1];
            return chainElement.Certificate;
        }

        private string Sign(string data, X509Certificate2 pkcs12)
        {
            string signature = string.Empty;
            RSA rsa = null;
            try
            {
                rsa = pkcs12.GetRSAPrivateKey();
                byte[] signed = rsa.SignData(
                    Encoding.UTF8.GetBytes(data), 
                    HashAlgorithmName.SHA256, 
                    RSASignaturePadding.Pkcs1);
                signature = Convert.ToBase64String(signed);
            }
            finally
            {
                rsa?.Clear();
            }
            
            return signature;
        }

        

        private bool Verify(string data, string base64Signature, X509Certificate2 keystore)
        {
            bool result;
            RSA rsa = null;
            try
            {
                rsa = keystore.GetRSAPublicKey();
                result = rsa.VerifyData(
                    Encoding.UTF8.GetBytes(data),
                    Convert.FromBase64String(base64Signature),
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);
            }
            finally
            {
                rsa?.Clear();
            }
             
            return result;
        }

        

        private X509Certificate2 GetSigningKeystore()
        {
            var store = new X509Certificate2(_storePath, _storePW);
            return store;
        }

        private X509Certificate2Collection GetGcaTrustChain()
        {
            var cms = new SignedCms();

            /* GCA .p7b uses PEM format, need to convert to DER binary format */
            byte[] chain = File.ReadAllBytes(_chainPath);
            string content = Encoding.UTF8.GetString(chain);
            string base64Content = content.Replace("-----BEGIN PKCS7-----", "").Replace("-----END PKCS7-----", "").Replace("\r", "").Replace("\n", "");
            byte[] decodedContent = Convert.FromBase64String(base64Content);    // DER binary format
            
            cms.Decode(decodedContent);  
            return cms.Certificates;
        }



    }
}
