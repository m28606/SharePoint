using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDataModel;
using TPCIP.CommonDomain;
using TPCIP.CommonServiceFacade.Mapping;
using TPCIP.ServiceLocatorInterfaces;

namespace TPCIP.CommonServiceFacade
{
    public class PulseFacade
    {
        private readonly IPulseAgent _pulsAgent;
        public PulseFacade(IServiceLocator serviceLocator)
        {
            _pulsAgent = serviceLocator.GetService<IPulseAgent>();

        }
        public PulseInfo getPulsUserDetails(string mid)
        {
            List<PulseDetails> bcpulsDetails = new List<PulseDetails>();
            bcpulsDetails = _pulsAgent.GetDepartmentDetails(mid.ToUpper());
            var userDetails = PulseMapper.MapPulsMapper(bcpulsDetails);
            return userDetails;
        }       
    }
}
