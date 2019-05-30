using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TPCIP.CommonDataModel;
using TPCIP.CommonServiceAgentInterfaces;

namespace TPCIP.CommonServiceAgents.Fakes
{
    public class FasAgent : IFasAgent
    {

        public virtual Stream Formdata(string lid, string profileid, string contactphone, string contactemail, FasoCreateRequest createRequest, string bookingdate, string bookingtime, bool attachtoOV)
        {

            var fakeData = @"callback({'success':true, 'fasoid':'20151029-000107'})";
            return ConvertStringToStream(fakeData);
        }

        protected readonly string FakeStatusOnFaultResponse = @"callback({'replyType':'FASO_FOUND','callbackMethod':'callBack','RequestType':'Open','DaysBack':'0','QueryParameter':{'Name':'LID','Value':'EF505893'},'analegTTcount':'4', 'FASO':[{'rebookingAllowed':false,'cancelAllowed':false,'FASOID':'20151029-000107','userId': 'u1000','Type':'S','FaultDescription':'MAWIS Kabelfejl: Automatisk oprettet af kunden fra TDC.dk driftsinfo','FaultCorrectionTime':'1753-01-01T00:00:00','SMSStatus':'Disabled','FeedbackSMSContactNumber':'pqr@tyu.com','MSOSContactNumber':'99999999','Status':'Onsite linie - Udgivet til MSOS','BookingCalendar':'DAG: 29-10-2015 07.30-16.00','BookingWeekday':'Torsdag','BookingDate':'2015-10-29T12:00:00','BookingTime':'730:1600','DIFASOID':'','DINextStatus':'','Technology':'','CustomerSegment':'','FASOCondition':'Open','OwnerId':'ODSND','OwnerFirst':'ODSND','OwnerLast':'ODSND','OwnerPhone':'','RemoteTime':'1753-01-01T00:00:00','OnsiteTime':'2015-10-29T16:00:00','CancelCode':'','FejlStatus':'Fejlretning er i gang','AfledtMark':'0','FASOEjerArbjGrp':'DSND','Modtagekode':'2 Afbrudt konstant','OprettelsesTidpunkt':'2015-10-29T07:58:30','X-No-Close':'0','X-Work-Complete':'1753-01-01T00:00:00','X-Msg-Sms':'False','SLA_name':'STANDARDSERVICE','SLA_text':'Dt ___-02D-04D-___ MF 08:00-16:00 __ K8 _ _','ToBePossible':'2','XPos':3,'YPos':1,'Data1':'2015-10-29','Data2':'07:30-16:00','noteHistory':'Test Comment 2016-04-02 12:13:24.12 Test Comment 2016-04-02 12:13:24.123 Test Comment 2016-04-02 12.13.24 Test Comment 2016-02-30 12:13:24.123 Test','waitingPosition' : '1','waitingReason' : 'abc'})";

        public virtual Stream updateFaultDescription(string fasoid, string number, string contactName, UpdateFaultRequest updatefaultRequest, string userId)
        {
            return ConvertStringToStream(FakeStatusOnFaultResponse);
        }

        private Stream ConvertStringToStream(string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public virtual Stream requestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck)
        {
            var fakeData = FakeStatusOnFaultResponse.Replace("'RequestType':'Open'", string.Format("'RequestType':'{0}'", type));
            return ConvertStringToStream(fakeData);
        }

        public virtual Stream getBookingTimes(string callbackMethod, string fasoid, string fromDate, string requestType, string appointmentType)
        {
            if ((requestType != "-1"))
            {
                var fakeData = @"callback({'fasoid':'20151029-000107','bookingCalendar':'FAST: 30-05-2014 14.00-15.00','rebookingAllowed':true,'bookingList':[
                {'weekday':'Fredag','bookingDate':'9/5','bookingTime':'08:00-10:00','fulldate':'09-05-2014','bookingId':'1b-ALLE DAGE 8-22E-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'9/5','bookingTime':'12:00-02:00','fulldate':'09-05-2014','bookingId':'1b-ALLE DAGE 8-22E1-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'11/5','bookingTime':'08:00-10:00','fulldate':'11-05-2014','bookingId':'1b-ALLE DAGE 8-22E2-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'11/5','bookingTime':'10:00-12:00','fulldate':'11-05-2014','bookingId':'1b-ALLE DAGE 8-22E3-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'12/5','bookingTime':'08:00-10:00','fulldate':'12-05-2014','bookingId':'1b-ALLE DAGE 8-22E4-Major','xWorkComplete':'1753-01-01T00:00:00' },
                {'weekday':'Fredag','bookingDate':'9/5','bookingTime':'10:00-12:00','fulldate':'09-05-2014','bookingId':'STANDARDSERVICE','xWorkComplete':'1853-01-01T00:00:00' },
                {'weekday':'Fredag','bookingDate':'10/5','bookingTime':'10:00-12:00','fulldate':'10-05-2014','bookingId':'STANDARDSERVICE','xWorkComplete':'1853-01-01T00:00:00' }]});";
                return ConvertStringToStream(fakeData);
            }
            else
            {
                var fakeData = @"callback(({'fasoid':'20151029-000107','bookingCalendar':'','callbackMethod':'callback','bookingCalendarName':'','rebookingAllowed':true,'bookingList':[{'weekday':'','bookingDate':'29/10','bookingId':'3-TIMERS SERVICE','fulldate':'29-10-2015'},{'weekday':'','bookingDate':'29/10','bookingId':'4-TIMERS SERVICE','fulldate':'29-10-2015'},{'weekday':'','bookingDate':'29/10','bookingId':'STRAKS FAKT. HV 8-16','fulldate':'29-10-2015'},{'weekday':'','bookingDate':'29/10','bookingId':'STRAKS FAKT. DØGN','fulldate':'29-10-2015'},{'weekday':'','bookingDate':'29/10','bookingId':'ALLE DAGE 0 24E BSKI:Minor','fulldate':'29-10-2015'},{'weekday':'','bookingDate':'29/10','bookingId':'ALLE DAGE 0 24E BSKI:Major','fulldate':'29-10-2015'}]});";
                return ConvertStringToStream(fakeData);
            }

        }

        public virtual Stream signUpForFeedbackSmsStatus(string callbackMethod, Boolean status, string number, string id, string lid, string feedbackemailid)
        {
            var fakeData = @"callback({'FASOID':'20151029-000107','DIFASOID':'','FeedbackSMSstatus':'0','FaultCorrectionTime':'Under planlægning'});";

            return ConvertStringToStream(fakeData);
        }

        public virtual Stream rebookTechnician(string callbackMethod, string fasoid, string date, string time, string serviceId, string appointmentType)
        {
            var fakeData = @"callback({'fasoid':'20151029-000107','success':true});";

            return ConvertStringToStream(fakeData);
        }

        public virtual Stream cancelFMFaso(string fasoId, RemarkData remark)
        {
            var fakeData = @"callback({'FASOID':'" + fasoId + "'})";
            return ConvertStringToStream(fakeData);
        }

        public virtual SimpleResult<string> notifyUser(string fasoId, string userid, string message)
        {
            return new SimpleResult<string>() { value = fasoId };
        }

        public virtual Stream getBookingTimeSlotsS6c(string userid, string lid, string profileName)
        {
            var fakeData = @"callback({'fasoid':'20151029-000107','bookingCalendar':'FAST: 30-05-2014 14.00-15.00','rebookingAllowed':true,'multi':'0','bookingList':[
                {'weekday':'Fredag','bookingDate':'9/5','bookingTime':'08:00-10:00','fulldate':'09-05-2014','bookingId':'1b-ALLE DAGE 8-22E-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'9/5','bookingTime':'12:00-02:00','fulldate':'09-05-2014','bookingId':'1b-ALLE DAGE 8-22E1-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'11/5','bookingTime':'08:00-10:00','fulldate':'11-05-2014','bookingId':'1b-ALLE DAGE 8-22E2-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'11/5','bookingTime':'10:00-12:00','fulldate':'11-05-2014','bookingId':'1b-ALLE DAGE 8-22E3-Major','xWorkComplete':'1753-01-01T00:00:00' },
 {'weekday':'Fredag','bookingDate':'12/5','bookingTime':'08:00-10:00','fulldate':'12-05-2014','bookingId':'1b-ALLE DAGE 8-22E4-Major','xWorkComplete':'1753-01-01T00:00:00' },
                {'weekday':'Fredag','bookingDate':'9/5','bookingTime':'10:00-12:00','fulldate':'09-05-2014','bookingId':'STANDARDSERVICE','xWorkComplete':'1853-01-01T00:00:00' },
                {'weekday':'Fredag','bookingDate':'10/5','bookingTime':'10:00-12:00','fulldate':'10-05-2014','bookingId':'STANDARDSERVICE','xWorkComplete':'1853-01-01T00:00:00' }]});";
            return ConvertStringToStream(fakeData);

        }
    }
}
