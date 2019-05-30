using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using TPCIP.ActiveUserGuides.DataModel;

namespace TPCIP.ActiveUserGuides.Fakes.BcWithFakeFallback
{
    public class CustomerAgent2 : CustomerAgent
    {
        private readonly ICustomerAgent _bcChannel;

        public CustomerAgent2(ICustomerAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override List<CustomerNote> getCustomerNotesByUserId(string userId, int page, int pageSize, string status)
        {
            try
            {
                return _bcChannel.getCustomerNotesByUserId(userId, page, pageSize, status);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getCustomerNotesByUserId(userId, page, pageSize, status);
        }
    }
}
