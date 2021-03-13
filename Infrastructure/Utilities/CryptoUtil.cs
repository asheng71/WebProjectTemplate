using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Utilities
{
    public static class CryptoUtil
    {
        public static byte[] EncryptAES128(byte[] data, string key, string iv)
        {
            byte[] result;
            Aes aes128Item = null;
            try
            {
                aes128Item = Aes.Create();
                aes128Item.Padding = PaddingMode.PKCS7;
                aes128Item.Mode = CipherMode.CBC;
                ICryptoTransform tf = aes128Item.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, tf, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    result = ms.ToArray();
                }
            }
            finally
            {
                if(aes128Item != null)
                {
                    aes128Item.Clear();
                }
            }

            return result;
        }
        public static byte[] EncryptAES256(byte[] data, string key, string iv)
        {
            byte[] result;

            Aes aes256Item = null;
            try
            {
                aes256Item = Aes.Create();
                aes256Item.Padding = PaddingMode.PKCS7;
                aes256Item.Mode = CipherMode.CBC;
                ICryptoTransform tf = aes256Item.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, tf, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    result = ms.ToArray();
                }
            }
            finally
            {
                if(aes256Item != null)
                {
                    aes256Item.Clear();
                }
                
            }

            return result;
        }
        public static byte[] DecryptAES128(byte[] data, string key, string iv)
        {
            byte[] result;

            Aes aes128Item = null;
            try
            {
                aes128Item = Aes.Create();
                aes128Item.Padding = PaddingMode.PKCS7;
                aes128Item.Mode = CipherMode.CBC;
                ICryptoTransform tf = aes128Item.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, tf, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    result = ms.ToArray();
                }
            }
            finally
            {
                if(aes128Item != null)
                {
                    aes128Item.Clear();
                }
                
            }

            return result;
        }
        public static byte[] DecryptAES256(byte[] data, string key, string iv)
        {
            byte[] result;

            Aes aes256Item = null;
            try
            {
                aes256Item = Aes.Create();
                aes256Item.Padding = PaddingMode.PKCS7;
                aes256Item.Mode = CipherMode.CBC;
                ICryptoTransform tf = aes256Item.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, tf, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    result = ms.ToArray();
                }
            }
            finally
            {
                if(aes256Item != null)
                {
                    aes256Item.Clear();
                }
                
            }

            return result;
        }

        public static string EncryptStringAES128(string plainText, string key, string iv)
        {
            byte[] result;
            Aes aes128Item = null;
            try
            {
                aes128Item = Aes.Create();
                aes128Item.Padding = PaddingMode.PKCS7;
                aes128Item.Mode = CipherMode.CBC;
                ICryptoTransform transform = aes128Item.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                byte[] bText = Encoding.UTF8.GetBytes(plainText);
                result = transform.TransformFinalBlock(bText, 0, bText.Length);
            }
            finally
            {
                if(aes128Item != null)
                {
                    aes128Item.Clear();
                }
                
            }

            return Convert.ToBase64String(result);
        }
        public static string EncryptStringAES256(string plainText, string key, string iv)
        {
            byte[] result;
            Aes aes256Item = null;
            try
            {
                aes256Item = Aes.Create();
                aes256Item.Padding = PaddingMode.PKCS7;
                aes256Item.Mode = CipherMode.CBC;
                ICryptoTransform transform = aes256Item.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                byte[] bText = Encoding.UTF8.GetBytes(plainText);
                result = transform.TransformFinalBlock(bText, 0, bText.Length);
            }
            finally
            {
                if(aes256Item != null)
                {
                    aes256Item.Clear();
                }
                
            }

            return Convert.ToBase64String(result);
        }
        public static string DecryptStringAES128(string base64String, string key, string iv)
        {
            Aes aes128Item = null;
            byte[] bEnBase64String = null;
            byte[] result = null;
            try
            {
                aes128Item = Aes.Create();
                aes128Item.Padding = PaddingMode.PKCS7;
                aes128Item.Mode = CipherMode.CBC;
                ICryptoTransform transform = aes128Item.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                bEnBase64String = Convert.FromBase64String(base64String);
                result = transform.TransformFinalBlock(bEnBase64String, 0, bEnBase64String.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($@"Decrypt Fail:{ex.Message}");
            }
            finally
            {
                if(aes128Item != null)
                {
                    aes128Item.Clear();
                }
                
            }

            return Encoding.UTF8.GetString(result);
        }
        public static string DecryptStringAES256(string base64String, string key, string iv)
        {
            byte[] result = null;
            byte[] bEnBase64String = null;

            Aes aes256Item = null;
            try
            {
                aes256Item = Aes.Create();
                aes256Item.Padding = PaddingMode.PKCS7;
                aes256Item.Mode = CipherMode.CBC;
                ICryptoTransform transform = aes256Item.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                bEnBase64String = Convert.FromBase64String(base64String);
                result = transform.TransformFinalBlock(bEnBase64String, 0, bEnBase64String.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($@"Decrypt Fail:{ex.Message}");
            }
            finally
            {
                if(aes256Item != null)
                {
                    aes256Item.Clear();
                }
                
            }

            return Encoding.UTF8.GetString(result);
        }
    }
}
