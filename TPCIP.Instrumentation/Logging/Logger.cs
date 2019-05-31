using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TPCIP.Instrumentation.Logging
{
    public static class Logger
    {
        public static void WriteLine(string message, LogCategory category)
        {
            Trace.WriteLine(DateTime.Now + Environment.NewLine + message + Environment.NewLine,
                category.ToString());
        }
    }
}
