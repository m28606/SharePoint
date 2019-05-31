using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TPCIP.Instrumentation.ExtensionMethods
{
    public static class StreamExtensions
    {
        public static string ReadAllAsString(this Stream stream)
        {
            stream.Position = 0;
            return new StreamReader(stream).ReadToEnd();
        }
    }
}
