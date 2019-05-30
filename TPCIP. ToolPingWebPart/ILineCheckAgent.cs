using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using TPCIP.CommonDataModel;
using TPCIP.ToolPingWebPart.DataModel;

namespace TPCIP.ToolPingWebPart
{
    [ServiceContract]
    public interface ILineCheckAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "ping/{sik}/{dslam}/{port}")]
        PingResultDataModel getPingResult(string sik, string dslam, string port);

        [OperationContract]
        [WebGet(UriTemplate = "serviceinfo/{lid}")]
        List<IhtsoaService> getService(string lid);

        [OperationContract]
        [WebGet(UriTemplate = "services/{lid}")]
        List<IhtsoaService> getServiceResult(string lid);

        [OperationContract]
        [WebGet(UriTemplate = "dsllinetest/{sik}?maxAge=0")]
        DslLineTestResult ihtsoaTestDslLine(string sik);

        [OperationContract]
        [WebGet(UriTemplate = "linediagnose?lid={lid}&source=LINESTATE")]
        LineDiagnoseResult getLineStateDiagnosticInformation(string lid);

        [OperationContract]
        [WebInvoke(UriTemplate = "releaseip/{sik}")]
        SimpleResult<string> releaseip(string sik);

       

        
    }

    [ServiceContract]
    public interface IOssiAgent
    {
        [OperationContract]
        [WebGet(UriTemplate = "ossi/dsl/pingtest?port={port}&dslam={dslam}&country={country}&tp={tp}&sik={sik}&channelId={channelId}")]
        DslPingTest getDslResult(string port, string dslam, string country, string tp, string sik, string channelId);

        //device/hybrid/linestatus/lid/{customerId}"; //
        [OperationContract]
        [WebInvoke(Method = "Get", UriTemplate = "device/hybrid/linestatus/lid/{customerId}")]
        HybridLineInfo hybridLineInfo(string customerId);
    
    }
}
