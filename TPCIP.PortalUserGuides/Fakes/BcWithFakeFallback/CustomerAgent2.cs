using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using TPCIP.PortalUserGuides.DataModel;

namespace TPCIP.PortalUserGuides.Fakes.BcWithFakeFallback
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

        public override CustomerNote getCustomerNote(string id)
        {
            try
            {
                var result = _bcChannel.getCustomerNote(id);
                if (result == null) throw new InvalidOperationException("_bcChannel.getCustomerNote returned null");
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getCustomerNote(id);
        }

        public override bool updateCustomerNote(CustomerNote customerNote)
        {
            try
            {
                return _bcChannel.updateCustomerNote(customerNote);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.updateCustomerNote(customerNote);
        }

    }
}
