using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    /// <summary>
    /// 異動類型 - 以 TRoad 結構元素的層面區分
    /// </summary>
    public enum ConfigCategory
    {
        Undefined = 0,

        /// <summary>
        /// TRoad 管理平臺
        /// </summary>
        TRoad = 1,
        /// <summary>
        /// 安控伺服器
        /// </summary>
        SS = 2,
        /// <summary>
        /// API 服務
        /// </summary>
        SSApi = 3,
        /// <summary>
        /// API 介接 (內&外)
        /// </summary>
        SSAgreement = 4
    }
}
