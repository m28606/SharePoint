using System.Collections.Generic;
namespace TPCIP.ToolPingWebPart.Domain
{
    public class PingResult
    {
        public List<pingData> resultPing { get; set; }
        public bool IsOk { get; set; }
        public string IsOkString { get; set; }
    }

    public class pingData
    {
        public string Role { get; set; }
        public string Message { get; set; }
        public string WanIP { get; set; }
    }
}
