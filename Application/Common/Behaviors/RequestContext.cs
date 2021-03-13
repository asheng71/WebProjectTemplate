using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Behaviors
{
    public class RequestContext
    {
        public RequestContext()
        {
            Items = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Items { get; set; }
    }
}
