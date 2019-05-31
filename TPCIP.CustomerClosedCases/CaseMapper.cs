using System;
using System.Globalization;
using TPCIP.CustomerClosedCases.DataModel;
using TPCIP.CustomerClosedCases.Domain;

namespace TPCIP.CustomerClosedCases
{
    public static class CaseMapper
    {
        public static Case MapCaseFromFas(FASO requeststatus, string lid = null)
        {
            return new Case
            {
                CreatedDateTime = DateTime.ParseExact(requeststatus.faultCorrectionTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Note = requeststatus.faultDescription,
                Id = requeststatus.fasoid,
                Type = CaseType.Faso,
                FeedbackSMSContactNumber = !string.IsNullOrEmpty(requeststatus.feedbackSMSContactNumber) ? requeststatus.feedbackSMSContactNumber : "",
                SelectedLID = lid,
            };
        }

        public static Case MapCaseFromETray(ETrayDocument document)
        {
            return new Case
            {
                CreatedDateTime = document.created,
                Note = document.note,
                Id = document.docId,
                Link = document.linkUrl,
                Type = CaseType.Etray,
            };
        }

        public static Case MapCaseFromColumbus(ColumbusClosedCase columbusOrder)
        {
            return new Case
            {
                CreatedDateTime = string.IsNullOrEmpty(columbusOrder.performedDate) ? DateTime.MinValue : DateTime.Parse(columbusOrder.performedDate),
                Note = string.IsNullOrEmpty(columbusOrder.customerCaseText) ? string.Empty : columbusOrder.customerCaseText,
                Id = string.IsNullOrEmpty(columbusOrder.orderNo) ? string.Empty : columbusOrder.orderNo,
                Type = CaseType.Columbus,
                CancellationDate = string.IsNullOrEmpty(columbusOrder.cancellationDate) ? string.Empty : columbusOrder.cancellationDate,
                FakDate = string.IsNullOrEmpty(columbusOrder.fakDate) ? string.Empty : columbusOrder.fakDate,
                Kusagkd = string.IsNullOrEmpty(columbusOrder.kusagkd) ? string.Empty : columbusOrder.kusagkd,
                NewLid = string.IsNullOrEmpty(columbusOrder.newLid) ? string.Empty : columbusOrder.newLid,
                OldLid = string.IsNullOrEmpty(columbusOrder.oldLid) ? string.Empty : columbusOrder.oldLid,
                OrderDate = string.IsNullOrEmpty(columbusOrder.orderDate) ? string.Empty : DateTime.Parse(columbusOrder.orderDate).ToString("dd-MM-yyyy"),
                OrderText = string.IsNullOrEmpty(columbusOrder.orderText) ? string.Empty : columbusOrder.orderText,
                Transcode = string.IsNullOrEmpty(columbusOrder.transcode) ? string.Empty : columbusOrder.transcode,
            };
        }

        public static Case MapActivityCaseFromBier(string note, string link, BierDocument document)
        {
            return new Case
            {
                CreatedDateTime = document.modtaget,
                Status = document.bierstatus,
                Id = document.aktivitet,
                Link = link + document.aktivitet,
                Type = CaseType.Bier,
                Note = note,
            };
        }

        public static Case MapFejlCaseFromBier(string note, string link, BierDocument document)
        {
            return new Case
            {
                CreatedDateTime = document.tid_oprettet,
                Status = document.status,
                Id = document.id,
                Link = link + document.id,
                Type = CaseType.Bier,
                Note = note,
            };
        }

    }
}
