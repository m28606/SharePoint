using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents.Fakes.BcWithFakeFallback
{
    public class FasAgent2 : FasAgent
    {
        private readonly IFasAgent _bcChannel;

        public FasAgent2(IFasAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override Stream Formdata(string lid, string profileid, string contactphone, string contactemail, FasoCreateRequest createRequest, string bookingdate, string bookingtime, bool attachtoOV)
        {
            try
            {
                return _bcChannel.Formdata(lid, profileid, contactphone, contactemail, createRequest, bookingdate, bookingtime, attachtoOV);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.Formdata(lid, profileid, contactphone, contactemail, createRequest, bookingdate, bookingtime, attachtoOV);
        }

        public override Stream getBookingTimes(string callbackMethod, string fasoid, string fromDate, string requestType, string appointmentType)
        {
            try
            {
                return _bcChannel.getBookingTimes(callbackMethod, fasoid, fromDate, requestType, appointmentType);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getBookingTimes(callbackMethod, fasoid, fromDate, requestType, appointmentType);
        }

        public override Stream getBookingTimeSlotsS6c(string userId, string lid, string profileName)
        {
            try
            {
                return _bcChannel.getBookingTimeSlotsS6c(userId, lid, profileName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getBookingTimeSlotsS6c(userId, lid, profileName);
        }

        public override Stream updateFaultDescription(string fasoId, string number, string contactName, UpdateFaultRequest updatefaultRequest, string userId)
        {
            try
            {
                return _bcChannel.updateFaultDescription(fasoId, number, contactName, updatefaultRequest, userId);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.updateFaultDescription(fasoId, number, contactName, updatefaultRequest, userId);
        }

        public override Stream requestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck)
        {
            try
            {
                return _bcChannel.requestStatusOnOpenClosedFault(val, queryBy.ToString(), daysBack, type, "callBack", performlinecheck);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.requestStatusOnOpenClosedFault(val, queryBy.ToString(), daysBack, type, "callBack", performlinecheck);
        }

        public override Stream signUpForFeedbackSmsStatus(string callbackMethod, Boolean status, string number, string id, string lid, string feedbackemailid)
        {
            try
            {
                return _bcChannel.signUpForFeedbackSmsStatus(callbackMethod, status, number, id, lid, feedbackemailid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.signUpForFeedbackSmsStatus(callbackMethod, status, number, id, lid, feedbackemailid);
        }

        public override Stream rebookTechnician(string callbackMethod, string fasoid, string date, string time, string serviceId, string appointmentType)
        {
            try
            {
                return _bcChannel.rebookTechnician(callbackMethod, fasoid, date, time, serviceId, appointmentType);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.rebookTechnician(callbackMethod, fasoid, date, time, serviceId, appointmentType);
        }

        public override Stream cancelFMFaso(string fasoid, RemarkData remark)
        {
            try
            {
                return _bcChannel.cancelFMFaso(fasoid, remark);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.cancelFMFaso(fasoid, remark);
        }

        public override SimpleResult<string> notifyUser(string fasoId, string userid, string message)
        {
            try
            {
                return _bcChannel.notifyUser(fasoId, userid, message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.notifyUser(fasoId, userid, message);
        }

    }
}
