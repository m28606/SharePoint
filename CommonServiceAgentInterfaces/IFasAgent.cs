using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CommonDataModel;

namespace TPCIP.CommonServiceAgentInterfaces
{
    [ServiceContract]
    public interface IFasAgent
    {

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/fas/fault/create?checksum=5be1c53826f37d5912624fac6fadcb9a&checkval=foobar&lid={lid}&profileid={profileId}&contactphone={contactPhone}&contactemail={contactEmail}&bookingDate={bookingDate}&bookingTime={bookingTime}&attachVO={attachtoOV}")]
        Stream Formdata(string lid, string profileid, string contactphone, string contactemail, FasoCreateRequest createRequest, string bookingDate, string bookingTime, bool attachtoOV);

        //from new North bound interface.
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fas/requeststatus?callback={callback}&requesttype={type}&daysback={daysBack}&queryby={queryBy}&queryvalue={val}&performlinecheck={performlinecheck}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a",
        BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        Stream requestStatusOnOpenClosedFault(string val, string queryBy, int daysBack, string type, string callback, bool performlinecheck);

        //from new North bound interface.
        [OperationContract]
        [WebGet(UriTemplate = "/fas/fault/{fasoid}/bookingslots?callback={callback}&appointmentType={appointmentType}&fromdate={date}&requestType={requestType}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a")]
        Stream getBookingTimes(string callback, string fasoid, string date, string requestType, string appointmentType);

        ////new S6c service for booking slots FAS
        [OperationContract]
        [WebGet(UriTemplate = "fas/creationtimeslot?userId={userId}&lid={lid}&profile={profileName}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a")]
        Stream getBookingTimeSlotsS6c(string userId, string lid, string profileName);

        //from new North bound interface.
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fas/fault/{fasoid}/rebook?callback={callback}&fulldate={date}&bookingTime={time}&appointmentType={appointmentType}&serviceId={serviceId}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        Stream rebookTechnician(string callback, string fasoid, string date, string time, string serviceId, string appointmentType);

        [OperationContract]
        [WebGet(UriTemplate = "fas/signupforfeedback?callback={callback}&enablesmsstatus={status}&feedbacksmscontactnumber={number}&fasoid={id}&lid={lid}&feedbackemailid={feedbackemailid}&checksum=5be1c53826f37d5912624fac6fadcb9a&checkval=foobar")]
        Stream signUpForFeedbackSmsStatus(string callback, Boolean status, string number, string id, string lid, string feedbackemailid);

        //from new North bound interface Nov Release.
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/fas/fault/{fasoid}/updatefaultdescription?contactNumber={number}&contactName={name}&userId={userId}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a")]
        Stream updateFaultDescription(string fasoid, string number, string name, UpdateFaultRequest updatefaultRequest, string userId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/fas/fault/{fasoId}/cancel?checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a")]
        Stream cancelFMFaso(string fasoId, RemarkData remark);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/fas/fault/id/{fasoId}/notification?user={userId}&message={message}&checkval=foobar&checksum=5be1c53826f37d5912624fac6fadcb9a")]
        SimpleResult<string> notifyUser(string fasoId, string userId, string message);
    }
}
