using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum MessageRecordSourceCategory
    {
        Undefined = 0,
        OM = 1,
        EM = 2,        
        ApplySS = 3,
        ReviewSS = 4,
        SSVersionNotification = 5
    }
}
