using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TPCIP.ToolPingWebPart.DataModel;

namespace TPCIP.ToolPingWebPart.Fakes.BcWithFakeFallback
{
    public class OssiAgent2 : LineCheckAgent
    {
        private readonly IOssiAgent _bcChannel;

        public OssiAgent2(IOssiAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override DslPingTest getDslResult(string port, string dslam, string country, string tp, string sik, string channelId)
        {
            try
            {
                return _bcChannel.getDslResult(port, dslam, country, tp, sik, channelId);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getDslResult(port, dslam, country, tp, sik, channelId);
        }

        public override HybridLineInfo hybridLineInfo(string lid)
        {
            try
            {
                return _bcChannel.hybridLineInfo(lid);
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex);
            }
            return base.hybridLineInfo(lid);
        }
    }
}
