using System;
using TPCIP.CustomerOpenCases.DataModel;
using System.Diagnostics;

namespace TPCIP.CustomerOpenCases.Fakes.BcWithFakeFallback
{
    public class BierAgent2 : BierAgent
    {
        private readonly IBierAgent _bcChannel;

        public BierAgent2(IBierAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override BierDocument[] GetBierTt(BierParameter param)
        {
            try
            {
                return _bcChannel.GetBierTt(param);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetBierTt(param);
        }

        public override BierDocument[] GetBierPo(BierParameter param)
        {
            try
            {
                return _bcChannel.GetBierPo(param);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetBierPo(param);
        }
    }
}
