using System;
using System.Linq;
using TPCIP.CommonServiceFacade;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.CustomerOpenCases.DataModel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TPCIP.CustomerOpenCases
{
    public class FasoFacade
    {
        public const int NotReadyErrorCode = 15000;

        private readonly IFasAgent _fasAgent;

        public FasoFacade(IServiceLocator serviceLocator)
        {
            _fasAgent = serviceLocator.GetService<IFasAgent>();
        }

        internal OpenClosedFaultsStatusReply GetFasoStatus(FasoRequestType type, FasoQueryBy queryBy, string val)
        {
            Stream fasoStream;
            try
            {
                fasoStream = _fasAgent.RequestStatusOnOpenClosedFault(val, queryBy.ToString(), 0, type.ToString(), "callBack", false);
            }
            catch (InvalidOperationException exception)
            {
                throw new BusinessCoreException(_fasAgent.GetType(), "requestStatusOnOpenClosedFault returnded unexpected response", exception);
            }

            var status = MapStream<OpenClosedFaultsStatusReply>(fasoStream);

            return status;
        }

        public T MapStream<T>(Stream requestStream)
        {
            var strResponse = new StreamReader(requestStream).ReadToEnd();

            var jsonStart = strResponse.IndexOf('{');
            var jsonEnd = strResponse.LastIndexOf('}') + 1;
            var strJsonResponse = strResponse.Substring(jsonStart, jsonEnd - jsonStart);//.Replace("\\", ""); //remove callback({someJson});
            try
            {
                JObject jResponse;
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
