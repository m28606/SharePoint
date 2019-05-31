using System;
using System.Diagnostics;
using TPCIP.CustomerOpenCases.DataModel;

namespace TPCIP.CustomerOpenCases.Fakes.BcWithFakeFallback
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
