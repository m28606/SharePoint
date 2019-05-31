using System;
using System.Collections.Generic;
using System.Diagnostics;
using TPCIP.CustomerClosedCases.DataModel;

namespace TPCIP.CustomerClosedCases.Fakes.BcWithFakeFallback
{
    public class CustomerClosedCaseAgent2 : CustomerClosedCaseAgent
    {
        private readonly ICustomerClosedCaseAgent _bcChannel;

        public CustomerClosedCaseAgent2(ICustomerClosedCaseAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override List<ColumbusClosedCase> GetCustomerClosedCase(string lid)
        {
            try
            {
                return _bcChannel.GetCustomerClosedCase(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetCustomerClosedCase(lid);
        }
    }
}
