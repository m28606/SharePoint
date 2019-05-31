using System;
using System.Diagnostics;
using TPCIP.CustomerClosedCases.DataModel;

namespace TPCIP.CustomerClosedCases.Fakes.BcWithFakeFallback
{
    public class EtrayAgent2 : EtrayAgent
    {
        private readonly IEtrayAgent _bcChannel;

        public EtrayAgent2(IEtrayAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override ETrayDocument[] GetETrayClosedCases(string lid)
        {
            try
            {
                return _bcChannel.GetETrayClosedCases(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetETrayClosedCases(lid);

        }
    }
}
