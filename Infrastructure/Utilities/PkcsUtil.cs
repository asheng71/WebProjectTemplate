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

        //public string GetIssuerName(X509Certificate2 cert)
        //{
        //    return cert.Issuer;
        //}

        public string GetCertName(X509Certificate2 cert)
        {
            // cert.FriendlyName 中文會亂碼

            if(cert == null)
            {
                return string.Empty;
            }

            string distinguisedName = cert.SubjectName.Name;
            string[] names = distinguisedName.Split(",");
            string result = string.Empty;
            try
            {
                for (int i = 0; i < names.Length; i++)
                {
                    string[] name = names[i].Split("=");
                    string key = name[0];
                    string value = name[1];
                    if (key == "SERIALNUMBER")
                    {
                        continue;
                    }
                    result = value;
                    break;
                }
            }
            catch { }
            

            return result;
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

        /* needs further investigation */

        //public string Sign(string data)
        //{

        //    string privateKey = File.ReadAllText(_keyPath)              //base64
        //        .Replace("\n", "")
        //        .Replace("\r", "")
        //        .Replace("-----BEGIN RSA PRIVATE KEY-----", "")
        //        .Replace("-----END RSA PRIVATE KEY-----", "");

        //    RSACryptoServiceProvider rsa = null;
        //    SHA256CryptoServiceProvider sha = null;
        //    try
        //    {
        //        rsa = new RSACryptoServiceProvider();
        //        sha = new SHA256CryptoServiceProvider();
        //        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

        //        byte[] signature = rsa.SignData(Encoding.UTF8.GetBytes(data), sha);

        //        byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(data));  // compute sha256 hash of the data
        //        byte[] signature2 = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        //        return Convert.ToBase64String(signature2);
        //    }
        //    finally
        //    {
        //        if (rsa != null)
        //        {
        //            rsa.Clear();
        //        }
        //        if (sha != null)
        //        {
        //            sha.Clear();
        //        }
        //    }

        //    return string.Empty;
        //}

        //public bool Verify(string data, string signature, X509Certificate2 certificate)
        //{

        //    RSACryptoServiceProvider rsa = null;
        //    SHA256CryptoServiceProvider sha = null;

        //    //RSACryptoServiceProvider publicKeyProvider = (RSACryptoServiceProvider)cert.PublicKey.Key;

        //    bool verifyData;
        //    try
        //    {
        //        rsa = new RSACryptoServiceProvider();
        //        sha = new SHA256CryptoServiceProvider();
        //        //rsa.ImportRSAPublicKey(certificate.GetPublicKey(), out _);
        //        //RSACryptoServiceProvider publicKeyProvider = (RSACryptoServiceProvider)certificate.PublicKey.Key;


        //        byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(data));  // compute sha256 hash of the data
        //        bool testHash = rsa.VerifyHash(hash, Convert.FromBase64String(signature), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);


        //        verifyData = rsa.VerifyData(Encoding.UTF8.GetBytes(data), sha, Convert.FromBase64String(signature));


        //    }
        //    finally
        //    {
        //        if (rsa != null)
        //        {
        //            rsa.Clear();
        //        }
        //        if (sha != null)
        //        {
        //            sha.Clear();
        //        }
        //    }
        //    return verifyData;
        //}


    }
}
