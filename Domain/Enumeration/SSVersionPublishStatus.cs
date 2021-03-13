using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum SSVersionPublishStatus
    {
        Undefined = 0,

        /// <summary>
        /// 已發布
        /// </summary>
        Published = 1,

        /// <summary>
        /// 未發布
        /// </summary>
        Unpublished = 2,
    }
}
