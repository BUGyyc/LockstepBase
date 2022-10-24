using System;

namespace Lockstep.Common.Logging
{

    [Flags]
    public enum LogSeverity
    {
        Exception = 1,
        Error = 2,
        Warn = 4,
        Info = 8,
        Trace = 0x10
    }

}