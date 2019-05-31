using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.PopUpFeedbackTab.DataModel;
using TPCIP.PopUpFeedbackTab.Domain;
using System.Diagnostics;

namespace TPCIP.PopUpFeedbackTab.Fakes.BcWithFakeFallback
{
    public class ColumbusAgent2 : ColumbusAgent
    {
        private readonly IColumbusAgent _bcChannel;

        public ColumbusAgent2(IColumbusAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override ColumbusOrderDroolsResult GetColumbusOpenOrders(string sources, string lid)
        {
            try
            {
                return _bcChannel.GetColumbusOpenOrders(sources, lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetColumbusOpenOrders(sources, lid);
        }

    }
}
