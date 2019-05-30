using System;
using System.Collections.Generic;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents
{
    public class PulseAgent : IPulseAgent
    {
        public virtual List<PulseDetails> GetDepartmentDetails(string Lid)
        {
            return new List<PulseDetails>
            {
                new PulseDetails
                {
                    name="Henrik Christensen1",
                    status="Aktiv",
                    loennr="719839",
                    a_number="M18155",
                    kontorfork="OOKB",
                    account_name="Sjælland Nord Coax",
                },
                new PulseDetails
                {
                    name="Henrik Christensen2",
                    status="Aktiv",
                    loennr="719839",
                    a_number="M18155",
                    kontorfork="OOKB",
                    account_name="Sjælland Nord Coax",
                },
                new PulseDetails
                {
                    name="Henrik Christensen3",
                    status="Aktiv",
                    loennr="719839",
                    a_number="M18155",
                    kontorfork="OOKB",
                    account_name="Sjælland Nord Coax",
                },
                         
            };
        }
    }
}
    
