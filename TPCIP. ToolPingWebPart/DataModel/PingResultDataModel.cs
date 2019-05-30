using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TPCIP.ToolPingWebPart.DataModel
{
    public class PingResultDataModel
    {
        [DataMember]
        public string pingUdstyr { get; set; }

        [DataMember]
        public List<string> pakkerSendt { get; set; }
    }
}


