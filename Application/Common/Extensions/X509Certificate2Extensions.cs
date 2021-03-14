using System.Security.Cryptography.X509Certificates;

namespace Application.Common.Extensions
{
    public static class X509Certificate2Extensions
    {

        public static string GetCertName(this X509Certificate2 cert)
        {
            string result = string.Empty;

            if (cert == null)
            {
                return result;
            }

            /* cert.FriendlyName 中文會亂碼 */
            string distinguisedName = cert.SubjectName.Name;  
            string[] names = distinguisedName.Split(",");
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
    }
}
