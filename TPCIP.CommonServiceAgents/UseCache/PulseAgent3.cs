using System;
using System.Collections.Generic;
using System.Threading;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonServiceFacade;

namespace TPCIP.CommonServiceAgents.UseCache
{
    public class PulseAgent3 : IPulseAgent
    {
        private readonly IPulseAgent _bcChannel;

        public PulseAgent3(IPulseAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }       

        public List<PulseDetails> GetDepartmentDetails(string mid)
        {
            List<PulseDetails> PulseDetails = CacheManager.Get<List<PulseDetails>>(CacheManager.CacheCategory.Pulse, mid, () => _bcChannel.GetDepartmentDetails(mid), 1440);
            return PulseDetails;
        }
    }
}
