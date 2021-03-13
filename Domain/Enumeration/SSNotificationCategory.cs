using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum SSNotificationCategory
    {
        Undefined = 0,
        
        /// <summary>
        /// OM異常事件
        /// </summary>
        OM = 1,

        /// <summary>
        /// EM異常事件
        /// </summary>
        EM = 2,

        /// <summary>
        /// 版本稽催
        /// </summary>
        Version = 3,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 9
    }
}
