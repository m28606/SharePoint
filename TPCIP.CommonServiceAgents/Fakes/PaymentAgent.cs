using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents
{
    public class PaymentAgent : IPaymentAgent
    {
        public virtual SubscribeAutoCard GetAutoCardSubscribtionDetails(string accountNo)
        {
            if (accountNo == "650403733")
            {
                return new SubscribeAutoCard()
                {
                    cyclePeriod = "quaterly",
                    billDeliveryInfo = new billDeliveryInfo() { deliveryType = "paper", email = "" },
                    notificationDeliveryInfo = new notificationDeliveryInfo() { notifyViaEmail = false, notifyViaSms = false, mobileNo = "12345678" },
                    letterDeliveryInfo = new letterDeliveryInfo() { deliveryType = "email", email = "mojuu@tdc.dk" },
                    subscriptions = new List<subscriptions>() { new subscriptions() { parentSegment = "PRIV" } },
                    payment = new Payment() { paymentMethod = "R", amount = 0.0, cardExpDate = "2024-06-30", cardNumberMasked = "XXXXXXXXXXXX0012" }
                };
            }
            else if (accountNo == "123785569")
            {
                return new SubscribeAutoCard()
                {
                    cyclePeriod = "halferly",
                    billDeliveryInfo = new billDeliveryInfo() { deliveryType = "Paper", email = "" },
                    notificationDeliveryInfo = new notificationDeliveryInfo() { notifyViaEmail = false, notifyViaSms = false, mobileNo = "09876547" },
                    letterDeliveryInfo = new letterDeliveryInfo() { deliveryType = "email", email = "tta@tdc.dk" },
                    subscriptions = new List<subscriptions>() { new subscriptions() { parentSegment = "PRIV" } },
                    payment = new Payment() { paymentMethod = "R", amount = 0.0, cardExpDate = "2024-06-30", cardNumberMasked = "XXXXXXXXXXXX0000" }
                };
            }
            else if (accountNo == "997456891")
            {
                return new SubscribeAutoCard()
                {
                    cyclePeriod = "monthly",
                    billDeliveryInfo = new billDeliveryInfo() { deliveryType = "paper", email = "" },
                    notificationDeliveryInfo = new notificationDeliveryInfo() { notifyViaEmail = true, notifyViaSms = true, mobileNo = "12345678" },
                    letterDeliveryInfo = new letterDeliveryInfo() { deliveryType = "email", email = "tta@tdc.dk" },
                    subscriptions = new List<subscriptions>() { new subscriptions() { parentSegment = "PRIV" } },
                    payment = new Payment() { paymentMethod = "C", amount = 0.0, cardExpDate = "2024-06-30", cardNumberMasked = "XXXXXXXXXXXX0200" }
                };
            }
            else if (accountNo == "203621955")
            {
                return new SubscribeAutoCard()
                {
                    cyclePeriod = "monthly",
                    billDeliveryInfo = new billDeliveryInfo() { deliveryType = "Email", email = "CD@tdc.dk" },
                    notificationDeliveryInfo = new notificationDeliveryInfo() { notifyViaEmail = true, notifyViaSms = false, mobileNo = "02345678" },
                    letterDeliveryInfo = new letterDeliveryInfo() { deliveryType = "email", email = "tta@tdc.dk" },
                    subscriptions = new List<subscriptions>() { new subscriptions() { parentSegment = "PRIV" } },
                    payment = new Payment() { paymentMethod = "C", amount = 0.0, cardExpDate = "2024-06-30", cardNumberMasked = "XXXXXXXXXXXX0100" }
                };
            }
            else
            {
                return new SubscribeAutoCard()
                {
                    cyclePeriod = "yearly",
                    billDeliveryInfo = new billDeliveryInfo() { deliveryType = "paper", email = "" },
                    notificationDeliveryInfo = new notificationDeliveryInfo() { notifyViaEmail = false, notifyViaSms = false, mobileNo = "02845678" },
                    letterDeliveryInfo = new letterDeliveryInfo() { deliveryType = "paper", email = "brev" },
                    subscriptions = new List<subscriptions>() { new subscriptions() { parentSegment = "PRIV" } },
                    payment = new Payment() { paymentMethod = "CH", amount = 0.0, cardExpDate = "2024-06-30", cardNumberMasked = "XXXXXXXXXXXX0011" }
                };
            }
        }


        public virtual SimpleResult<string> DeleteAutomaticCardPayment(string accountNo)
        {
            return new SimpleResult<string>() { value = "http status is 200 OK" };
        }

    }
}
