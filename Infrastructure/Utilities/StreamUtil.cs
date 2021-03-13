using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Utilities
{
    public static class StreamUtil
    {
        public static string ReadToString(IFormFile file)
        {
            if(file == null)
            {
                throw new ArgumentNullException();
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                return reader.ReadToEnd();
            }
        }

        public static byte[] ReadByteArray(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException();
            }

            byte[] result;
            using(var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                result = stream.ToArray();
            }

            return result;
        }
    }
}
