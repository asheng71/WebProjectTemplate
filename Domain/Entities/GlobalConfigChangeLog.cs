using Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GlobalConfigChangeLog
    {
        public long Id { get; set; }

        public ConfigCategory ConfigCategory { get; set; }

        public string Description { get; set; }

        public string ReferenceId { get; set; }

        public TRoadStateDefinitions StateChange { get; set; }

        public string GcaOid { get; set; }

        public string SecurityServerId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime LogTime { get; set; }

        public long? OMSerialId { get; set; }
        public string GlobalConfigJson { get; set; }

    }
}
