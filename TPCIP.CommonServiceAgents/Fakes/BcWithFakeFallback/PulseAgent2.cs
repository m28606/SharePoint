using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonServiceAgents;


namespace TPCIP.CommonServiceAgents.Fakes.BcWithFakeFallback
{
  public  class PulseAgent2:PulseAgent
    {
        private readonly IPulseAgent _bcChannel;

        public PulseAgent2(IPulseAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }
        public override List<PulseDetails> GetDepartmentDetails(string Lid)
        {
            try
            {
                return _bcChannel.GetDepartmentDetails(Lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetDepartmentDetails(Lid);
        }
    }
}
