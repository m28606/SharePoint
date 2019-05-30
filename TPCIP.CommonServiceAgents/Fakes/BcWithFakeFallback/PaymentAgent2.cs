using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents
{
    public class PaymentAgent2 : PaymentAgent
    {
        private readonly IPaymentAgent _bcChannel;

        public PaymentAgent2(IPaymentAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override SubscribeAutoCard GetAutoCardSubscribtionDetails(string accountNo)
        {
            try
            {
                return _bcChannel.GetAutoCardSubscribtionDetails(accountNo);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.GetAutoCardSubscribtionDetails(accountNo);
        }

        public override SimpleResult<string> DeleteAutomaticCardPayment(string accountNo)
        {
            try
            {
                return _bcChannel.DeleteAutomaticCardPayment(accountNo);
            }

            catch { }

            return base.DeleteAutomaticCardPayment(accountNo);
        }
    }
}
