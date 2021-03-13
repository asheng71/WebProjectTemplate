using Domain.Common;
using Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationLog
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
        public string Description { get; set; }
    }
}
