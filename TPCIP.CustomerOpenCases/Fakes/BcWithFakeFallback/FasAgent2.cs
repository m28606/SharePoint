using System;
using System.Diagnostics;
using System.IO;

namespace TPCIP.CustomerOpenCases.Fakes.BcWithFakeFallback
{
    public class FasAgent2 : FasAgent
    {
        private readonly IFasAgent _bcChannel;

        public FasAgent2(IFasAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override Stream RequestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck)
        {
            try
            {
                return _bcChannel.RequestStatusOnOpenClosedFault(val, queryBy, daysBack, type, "callBack", performlinecheck);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.RequestStatusOnOpenClosedFault(val, queryBy, daysBack, type, "callBack", performlinecheck);
        }
    }
}
