using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utilities
{
    public static class DateTimeExtension
    {
        public static string ToTwString(this DateTime dt)
        {            
            return $"{(dt.Year - 1911).ToString("D3")}{dt.ToString("/MM/dd HH:mm:ss")}";
        }

        public static string ToTwDateString(this DateTime dt)
        {
            return $"{(dt.Year - 1911).ToString("D3")}{dt.ToString("/MM/dd")}";
        }

        public static string ToTwFormalDateString(this DateTime dt)
        {
            return $"民國{(dt.Year - 1911).ToString("D3")}年{dt.ToString("%M")}月{dt.ToString("%d")}日";
        }
    }
}
