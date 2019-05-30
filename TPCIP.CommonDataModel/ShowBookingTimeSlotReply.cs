using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TPCIP.CommonDataModel
{
    [Serializable]
    [DataContract]
    public class ShowBookingTimeSlotReply
    {
        [DataMember(Name = "appointmentType")]
        public string appointmentType { get; set; }

        [DataMember(Name = "bookingCalendar")]
        public string bookingCalendar { get; set; }

        [DataMember(Name = "bookingCalendarName")]
        public string bookingCalendarName { get; set; }

        [DataMember(Name = "fasoid")]
        public string fasoid { get; set; }

        [DataMember(Name = "rebookingAllowed")]
        public bool rebookingAllowed { get; set; }

        [DataMember(Name = "bookingList")]
        public List<BookingTimeSlot> bookingList { get; set; }

        [DataMember(Name = "lid")]
        public string lid { get; set; }

        [DataMember(Name = "service")]
        public string service { get; set; }

        [DataMember(Name = "multi")]
        public string multi { get; set; }

    }
}
