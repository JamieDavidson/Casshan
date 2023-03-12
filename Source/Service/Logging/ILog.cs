using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casshan.Logging
{
    internal interface ILog
    {
        void Log(string message, LogLevel level);
    }
}
