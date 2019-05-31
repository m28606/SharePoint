using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TPCIP.CommonDomain;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceFacade.Mapping
{

    static public class PulseMapper
    {
        public static PulseInfo MapPulsMapper(List<PulseDetails> bcpulsDetails)
        {
            var pulse = bcpulsDetails.Select(o => new PulseInfo
               {
                   Name = o.name,
                   Status = o.status,
                   Kontorfork = o.kontorfork,
                   SalaryNo = o.loennr,
                   A_number = o.a_number,
                   AccountName = o.account_name,

               }).FirstOrDefault();
            return pulse;
        }
    }
}