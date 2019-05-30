using System;
using System.Collections.Generic;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents
{
    public class CustomerDataAgent : ICustomerDataAgent
    {
        public virtual List<EmergencySubscription> getEmergencyCustomerInformation(string subscriptionId, string accountNo, string madId)
        {
            var emergencySubscription = new List<EmergencySubscription>() { 
                new EmergencySubscription { rootLid = "YL123456", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "EM120268", parentAccountNo = 312345678, customerName = "*Jozko *Mrkvicka", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "4410 Bratislava J", fullAddress = "Bratislava 40" },
                new EmergencySubscription { rootLid = "87341127", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "YL123456", parentAccountNo = 612345678, customerName = "*Jozsoo *Mrkvic", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "4410 Bratislava J", fullAddress = "Bratislava 40" },
                new EmergencySubscription { rootLid = "EM120268", parentAccountNo = 312345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "87341127", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "YL123456", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "EM120268", parentAccountNo = 312345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "YL123456", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "EM120268", parentAccountNo = 312345678, customerName = "*Jozko *Mrkvicka", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "4410 Bratislava J", fullAddress = "Bratislava 40" },
                new EmergencySubscription { rootLid = "87341127", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "YL123456", parentAccountNo = 612345678, customerName = "*Jozsoo *Mrkvic", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "4410 Bratislava J", fullAddress = "Bratislava 40" },
                new EmergencySubscription { rootLid = "EM120268", parentAccountNo = 312345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "87341127", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "YL123456", parentAccountNo = 612345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                new EmergencySubscription { rootLid = "EM120268", parentAccountNo = 312345678, customerName = "*Marian *Miezga", madId = "0A3F50C2FABD32B8E0440003BA298018", zipCity = "8310 Tranbjerg J", fullAddress = "Sletvej 30" },
                         
            };
            return emergencySubscription;

        }
    }
}
    
