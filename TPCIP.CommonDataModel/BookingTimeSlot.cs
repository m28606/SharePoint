using System;
using System.Runtime.Serialization;

namespace TPCIP.CommonDataModel
{
    [Serializable]
    [DataContract]
    public class BookingTimeSlot
    {
        [DataMember(Name = "weekday")]
        public string weekday { get; set; }

        [DataMember(Name = "bookingDate")]
        public string bookingDate { get; set; }

        [DataMember(Name = "bookingTime")]
        public string bookingTime { get; set; }

        [DataMember(Name = "fulldate")]
        public string fulldate { get; set; }

        [DataMember(Name = "bookingId")]
        public string bookingId { get; set; }

        [DataMember(Name = "bookingWeekday")]
        public string bookingWeekday { get; set; }
    }
}
