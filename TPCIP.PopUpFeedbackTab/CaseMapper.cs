using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.PopUpFeedbackTab.DataModel;
using TPCIP.PopUpFeedbackTab.Domain;
using System.Globalization;

namespace TPCIP.PopUpFeedbackTab
{
   public static class CaseMapper
    {
        public static Case MapCaseFromFas(FASO requeststatus)
        {
            return new Case
            {
                CreatedDateTime = DateTime.ParseExact(requeststatus.faultCorrectionTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Note = requeststatus.faultDescription,
                Id = requeststatus.fasoid,
                Type = CaseType.Faso,
                FeedbackSMSContactNumber = !string.IsNullOrEmpty(requeststatus.feedbackSMSContactNumber) ? requeststatus.feedbackSMSContactNumber : "",
            };
        }
    }
}
