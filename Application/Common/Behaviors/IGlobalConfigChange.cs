using System;
using System.Collections.Generic;
using System.Text;

namespace Application.GlobalConfig
{
    public interface IGlobalConfigChange
    {
        public GlobalConfigChangeContext ConfigContext { get; set; }

    }
}
