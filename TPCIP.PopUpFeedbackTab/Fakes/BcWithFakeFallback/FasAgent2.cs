using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;


namespace TPCIP.PopUpFeedbackTab.Fakes.BcWithFakeFallback
{
    public class FasAgent2 : FasAgent
    {
        private readonly IFasAgent _bcChannel;

        public FasAgent2(IFasAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override Stream requestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck)
        {
            try
            {
                return _bcChannel.requestStatusOnOpenClosedFault(val, queryBy.ToString(), daysBack, type, "callBack", performlinecheck);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.requestStatusOnOpenClosedFault(val, queryBy.ToString(), daysBack, type, "callBack", performlinecheck);
        }
    }
}
