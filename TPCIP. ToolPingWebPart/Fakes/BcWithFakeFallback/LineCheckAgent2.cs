using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using TPCIP.ToolPingWebPart.DataModel;
using TPCIP.CommonDataModel;

namespace TPCIP.ToolPingWebPart.Fakes.BcWithFakeFallback
{
    public class LineCheckAgent2 : LineCheckAgent
    {
        private readonly ILineCheckAgent _bcChannel;

        public LineCheckAgent2(ILineCheckAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }

        public override PingResultDataModel getPingResult(string sik, string dslam, string port)
        {
            try
            {
                return _bcChannel.getPingResult(sik, dslam, port);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getPingResult(sik, dslam, port);
        }

        public override List<IhtsoaService> getService(string lid)
        {
            try
            {
                return _bcChannel.getService(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getService(lid);
        }

        public override List<IhtsoaService> getServiceResult(string lid)
        {
            try
            {
                return _bcChannel.getServiceResult(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getServiceResult(lid);
        }

        public override DslLineTestResult ihtsoaTestDslLine(string sik)
        {
            try
            {
                return _bcChannel.ihtsoaTestDslLine(sik);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.ihtsoaTestDslLine(sik);
        }

        public override LineDiagnoseResult getLineStateDiagnosticInformation(string lid)
        {
            try
            {
                return _bcChannel.getLineStateDiagnosticInformation(lid);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.getLineStateDiagnosticInformation(lid);
        }

        public override SimpleResult<string> releaseip(string sik)
        {
            try
            {
                return _bcChannel.releaseip(sik);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            return base.releaseip(sik);
        }

        
    }

}
