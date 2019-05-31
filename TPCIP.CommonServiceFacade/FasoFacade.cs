using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
//using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPCIP.CommonServiceFacade;
using System.ComponentModel;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.CommonServiceAgentInterfaces;
using TPCIP.CommonDomain;
using TPCIP.CommonServiceFacade.Mapping;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceFacade
{
    public class FasoFacade
    {
        public const int NotReadyErrorCode = 15000;
        private readonly IFasAgent _fasAgent;
        //private readonly IArticleProviderAgent _articleProviderAgent;

        public FasoFacade(IServiceLocator serviceLocator)
        {
            _fasAgent = serviceLocator.GetService<IFasAgent>();
            //_articleProviderAgent = serviceLocator.GetService<IArticleProviderAgent>();

        }

        public List<AvailableBookingTimeList> GetAvailableBookingTimes(string fasoId, string fromDate, string requestType, string appointmentType)
        {
            var bookingTimesFromBc = GetAvailableBookingTimesFromBc("callBack", fasoId, fromDate, requestType, appointmentType);

            var bookingTimes = FasoMapper.MappBookingTimes(bookingTimesFromBc).OrderBy(item => item.Beginning);

            var availableDates = bookingTimes.GroupBy(item => item.Beginning.ToString("d"))
                .Select(dateTime => new AvailableBookingTimeList
                {
                    Date = dateTime.Key,
                    Times = dateTime.Select(g => g.Beginning.ToString("HH:mm") + "-" + g.End.ToString("HH:mm")).ToList()
                }).Take(2000)
                .ToList();

            return availableDates;
        }

        internal ShowBookingTimeSlotReply GetAvailableBookingTimesFromBc(string callbackMethod, string fasoid, string fromDate, string requestType, string appointmentType)
        {
            try
            {
                var fasoStream = _fasAgent.getBookingTimes("callback", fasoid, fromDate, requestType, appointmentType);
                var status = MapStream<ShowBookingTimeSlotReply>(fasoStream);

                if (status != null) return status;
                return null;
            }
            catch { return null; }
        }

        public List<AvailableBookingTimeList> GetAvailableBookingTimesNew(string userid, string lid, string profileName, out string Multi)
        {
            var bookingTimesFromBc = GetAvailableBookingTimesFromBcS6c(userid, lid, profileName);
            Multi = bookingTimesFromBc.multi;
            var bookingTimes = FasoMapper.MappBookingTimes(bookingTimesFromBc).OrderBy(item => item.Beginning);
            var availableDates = bookingTimes.GroupBy(item => item.Beginning.ToString("d"))
                .Select(dateTime => new AvailableBookingTimeList
                {
                    Date = dateTime.Key,
                    Times = dateTime.Select(g => g.Beginning.ToString("HH:mm") + "-" + g.End.ToString("HH:mm")).ToList()
                }).Take(2000)
                .ToList();

            return availableDates;
        }

        internal ShowBookingTimeSlotReply GetAvailableBookingTimesFromBcS6c(string userid, string lid, string profileName)
        {
            try
            {
                var fasoStream = _fasAgent.getBookingTimeSlotsS6c(userid, lid, profileName);
                var status = MapStream<ShowBookingTimeSlotReply>(fasoStream);

                if (status != null) return status;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public T MapStream<T>(Stream requestStream)
        {
            var strResponse = new StreamReader(requestStream).ReadToEnd();

            int jsonStart = strResponse.IndexOf('{');
            int jsonEnd = strResponse.LastIndexOf('}') + 1;
            var strJsonResponse = strResponse.Substring(jsonStart, jsonEnd - jsonStart);//.Replace("\\", ""); //remove callback({someJson});
            try
            {
                JObject jResponse = null;
                try
                {
                    jResponse = JsonConvert.DeserializeObject<JObject>(strJsonResponse);
                }
                catch
                {
                    strJsonResponse = strResponse.Substring(jsonStart, jsonEnd - jsonStart);
                    jResponse = JsonConvert.DeserializeObject<JObject>(strJsonResponse);
                }

                if (jResponse.Properties().Any(p => p.Name == "error") && (int)jResponse["error"]["code"] != 81520)
                {
                    var errorData = new BusinessCoreExceptionData
                    {
                        Code = (int)jResponse["error"]["code"],
                        Message = (string)jResponse["error"]["message"],
                        StackTrace = (string)jResponse["error"]["stack trace"]
                    };

                    throw new BusinessCoreException(typeof(IFasAgent), errorData.Message, errorData);
                }

                return jResponse.ToObject<T>();
            }
            catch (JsonReaderException ex)
            {
                throw new BusinessCoreException(typeof(IFasAgent), "Response returned from FAS is not valid JSON:\n" + strResponse, ex);
            }
        }

    }
}
