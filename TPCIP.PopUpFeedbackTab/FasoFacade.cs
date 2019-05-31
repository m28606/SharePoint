using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TPCIP.PopUpFeedbackTab.DataModel;
using TPCIP.PopUpFeedbackTab.Domain;
using TPCIP.CommonServiceFacade;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPCIP.ServiceLocatorInterfaces;

namespace TPCIP.PopUpFeedbackTab
{
    public class FasoFacade
    {
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
                fasoStream = _fasAgent.requestStatusOnOpenClosedFault(val, queryBy.ToString(), 0, type.ToString(), "callBack", false);
            }
            catch (InvalidOperationException exception)
            {
                throw new BusinessCoreException(_fasAgent.GetType(), "requestStatusOnOpenClosedFault returnded unexpected response", exception);
            }

            var status = MapStream<OpenClosedFaultsStatusReply>(fasoStream);

            if (status != null) return status;
            return null;
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
