using Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace Infrastructure.Tests.Utilities
{
    public class PkcsUtilTest
    {
        IConfigurationRoot _config;

        public PkcsUtilTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }


        [Fact]
        public void Pkcs_SignedData_IsSelfVerifiable()
        {
            // Arrange
            var pkcs = new PkcsUtil(_config);

            // Act
            string base64Hash = pkcs.Sign("hello");
            bool isVerified = pkcs.Verify("hello", base64Hash);

            // Assert
            Assert.True(isVerified);
        }

        [Fact]
        public void Pkcs_SSLCertificate_Disguishable()
        {
            // Arrange
            var config = _config as IConfiguration;
            string accurateCertPath = config.GetValue<string>("AccurateSslCertificate");
            string inaccurateCertPath = config.GetValue<string>("InaccurateSslCertificate");

            var sslCert = new X509Certificate2(accurateCertPath);
            var nonSslCert = new X509Certificate2(inaccurateCertPath);
            var pkcs = new PkcsUtil(_config);

            // Act
            bool shouldBeTrue = pkcs.IsSSLServerCertificate(sslCert);
            bool shouldBeFalse = pkcs.IsSSLServerCertificate(nonSslCert);

            // Assert
            Assert.True(shouldBeTrue);
            Assert.False(shouldBeFalse);
        }
    }
}
