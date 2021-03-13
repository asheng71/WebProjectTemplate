using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum SSVersionUpdateStatus
    {
        Undefined = 0,

        /// <summary>
        /// 未更新
        /// </summary>
        Outdated = 1,

        /// <summary>
        /// 已更新
        /// </summary>
        Updated = 2,
    }
}
