using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TPCIP.PopUpFeedbackTab.DataModel;
using TPCIP.PopUpFeedbackTab.Domain;

namespace TPCIP.PopUpFeedbackTab.Fakes
{
    public class FasAgent : IFasAgent
    {
        public virtual Stream requestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck)
        {
            var fakeData = FakeStatusOnFaultResponse.Replace("'RequestType':'Open'", string.Format("'RequestType':'{0}'", type));
            return ConvertStringToStream(fakeData);
        }

        protected readonly string FakeStatusOnFaultResponse = @"callback({'replyType':'FASO_FOUND','callbackMethod':'callBack','RequestType':'Open','DaysBack':'0','QueryParameter':{'Name':'LID','Value':'EF505893'},'analegTTcount':'4', 'FASO':[{'rebookingAllowed':false,'cancelAllowed':false,'FASOID':'20151029-000107','userId': 'u1000','Type':'S','FaultDescription':'MAWIS Kabelfejl: Automatisk oprettet af kunden fra TDC.dk driftsinfo','FaultCorrectionTime':'1753-01-01T00:00:00','SMSStatus':'Disabled','FeedbackSMSContactNumber':'pqr@tyu.com','MSOSContactNumber':'99999999','Status':'Onsite linie - Udgivet til MSOS','BookingCalendar':'DAG: 29-10-2015 07.30-16.00','BookingWeekday':'Torsdag','BookingDate':'2015-10-29T12:00:00','BookingTime':'730:1600','DIFASOID':'','DINextStatus':'','Technology':'','CustomerSegment':'','FASOCondition':'Open','OwnerId':'ODSND','OwnerFirst':'ODSND','OwnerLast':'ODSND','OwnerPhone':'','RemoteTime':'1753-01-01T00:00:00','OnsiteTime':'2015-10-29T16:00:00','CancelCode':'','FejlStatus':'Fejlretning er i gang','AfledtMark':'0','FASOEjerArbjGrp':'DSND','Modtagekode':'2 Afbrudt konstant','OprettelsesTidpunkt':'2015-10-29T07:58:30','X-No-Close':'0','X-Work-Complete':'1753-01-01T00:00:00','X-Msg-Sms':'False','SLA_name':'STANDARDSERVICE','SLA_text':'Dt ___-02D-04D-___ MF 08:00-16:00 __ K8 _ _','ToBePossible':'2','XPos':3,'YPos':1,'Data1':'2015-10-29','Data2':'07:30-16:00','noteHistory':'Test Comment 2016-04-02 12:13:24.12 Test Comment 2016-04-02 12:13:24.123 Test Comment 2016-04-02 12.13.24 Test Comment 2016-02-30 12:13:24.123 Test','waitingPosition' : '1','waitingReason' : 'abc'})";

        private Stream ConvertStringToStream(string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
