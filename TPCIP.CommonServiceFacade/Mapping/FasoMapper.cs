using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.CommonDomain;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceFacade.Mapping
{
    public class FasoMapper
    {
        public const long _REST_DEFAULT_YEAR = 1753;
        public const string _DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss.ff";
        public const string _DATE_TIME_FORMAT_FAS = "dd-MM-yyyy HH:mm";

        public static List<BookingTime> MappBookingTimes(ShowBookingTimeSlotReply bookingTimes)
        {
            var result = new List<BookingTime>();

            if (bookingTimes != null && bookingTimes.bookingList != null)
            {
                foreach (var booking in bookingTimes.bookingList)
                {
                    var time = booking.bookingTime != null ? booking.bookingTime.Split(new[] { '-' }) : new string[2];
                    result.Add(new BookingTime
                    {
                        Beginning = Convert.ToDateTime(booking.fulldate + " " + time[0]),
                        End = Convert.ToDateTime(booking.fulldate + " " + time[1]),
                    });
                }
            }

            return result;
        }
    }
}
