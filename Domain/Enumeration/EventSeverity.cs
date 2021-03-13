using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum EventSeverity
    {
        Undefined = 0,
        /// <summary>
        /// 嚴重
        /// </summary>
        Critical = 1,

        /// <summary>
        /// 重大
        /// </summary>
        Major = 2,

        /// <summary>
        /// 中等
        /// </summary>
        Minor = 3
    }
}
