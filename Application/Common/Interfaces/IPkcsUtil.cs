using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IPkcsUtil
    {
        public bool Verify(string data, string base64Signature);

        /// <summary>
        /// Sign data with given public/private key using RSA and SHA256
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pkcs12">pkcs12 certificate/key store</param>
        /// <returns>base64 hash value</returns>
        public string Sign(string data);


        public X509Certificate2 GetRootCACertificate(X509Certificate2 cert);

        public string GetCertName(X509Certificate2 cert);
    }
}
