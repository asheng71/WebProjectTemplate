using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    /// <summary>
    /// 安控伺服器開通狀態
    /// </summary>
    public enum SecurityServerActivationState
    {
        Undefined = 0,

        /// <summary>
        /// 申請中 (用印文件未上傳)
        /// </summary>
        Applying = 1,
        /// <summary>
        /// 待審查 (用印文件已上傳)
        /// </summary>
        Reviewing = 2,
        /// <summary>
        /// 已開通
        /// </summary>
        Activated = 3,
        /// <summary>
        /// 暫不開通
        /// </summary>
        NotActivated = 4
    }
}
