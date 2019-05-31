using System;

namespace TPCIP.CustomerClosedCases.Domain
{
    /// <summary>
    /// task 70
    /// </summary>
    public class Case
    {
        public DateTime CreatedDateTime { get; set; }

        public string Created
        {
            get
            {
                if (CreatedDateTime == DateTime.MinValue) return string.Empty;
                return CreatedDateTime.ToString("dd:MM:yy");
            }
        }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Id { get; set; }
        public string Link { get; set; }
        public CaseType Type { get; set; }
        public string ErrorType { get; set; }
        public Exception BcException { get; set; }
        public string FeedbackSMSContactNumber { get; set; }
        public string SelectedLID { get; set; }

        #region This region belongs to ColumbusCloseCases

        public string NewLid { get; set; }

        public string OldLid { get; set; }

        public string Kusagkd { get; set; }

        public string CustomerCaseText { get; set; }

        public string Transcode { get; set; }

        public string OrderText { get; set; }

        public string OrderDate { get; set; }

        public string FakDate { get; set; }

        public string CancellationDate { get; set; }

        #endregion
    }
}
