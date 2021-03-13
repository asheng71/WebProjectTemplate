using Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Infrastructure.Tests.Utilities
{
    public class PkcsUtilTest
    {
        public class PkcsConfigurations : IConfiguration
        {
            private Dictionary<string, string> Items { get; set; }

            public PkcsConfigurations()
            {
                Items = new Dictionary<string, string>()
                {
                    { "SigingKeyStore", "./TestAssests/ca-self.p12" },
                    { "SigingKeyStorePW", "hello" }
                };
            }

            public string this[string key] { get => Items[key]; set => Items[key] = value; }

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new NotImplementedException();
            }

            public IConfigurationSection GetSection(string key)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void Pkcs_SignedData_IsSelfVerifiable()
        {
            string dir = AppContext.BaseDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            IConfigurationRoot config = builder.Build();
            
            var pkcs = new PkcsUtil(config);

            string base64Hash = pkcs.Sign("hello");
            bool isVerified = pkcs.Verify("hello", base64Hash);

            Assert.True(isVerified);
        }
    }
}
