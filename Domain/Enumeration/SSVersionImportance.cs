using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum SSVersionImportance
    {
        Undefined = 0,
        
        /// <summary>
        /// 一般
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 重要
        /// </summary>
        Important = 2,
    }
}
