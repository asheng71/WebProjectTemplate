using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum RoleApplicationState
    {
        Undefined = 0,

        /// <summary>
        /// 申請中 (待審查)
        /// </summary>
        Applying = 1,        
        /// <summary>
        /// 已開通 (通過)
        /// </summary>
        Approved = 2,
        /// <summary>
        /// 暫不開通 (駁回)
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// 再申請 (待複審)
        /// </summary>
        Reapplying = 4,
        /// <summary>
        /// 不開通 (再駁回)
        /// </summary>
        Rerejected = 5,
        /// <summary>
        /// 取消 (取消)
        /// </summary>
        Cancel = 6
    }
}
