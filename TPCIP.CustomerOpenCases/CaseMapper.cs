using System;
using System.Globalization;
using TPCIP.CustomerOpenCases.DataModel;
using TPCIP.CustomerOpenCases.Domain;

namespace TPCIP.CustomerOpenCases
{
    public static class CaseMapper
    {
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

        public static Case MapCaseFromFas(FASO requeststatus, string lid = null)
        {
            return new Case
            {
                CreatedDateTime = DateTime.ParseExact(requeststatus.oprettelsesTidpunkt, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Note = requeststatus.oprettelsesTidpunkt,
                Id = requeststatus.fasoid,
                Type = CaseType.Faso,
                FeedbackSMSContactNumber = !string.IsNullOrEmpty(requeststatus.feedbackSMSContactNumber) ? requeststatus.feedbackSMSContactNumber : "",
                SelectedLID = lid
            };
        }

        public static Case MapCaseFromCu(ColumbusOrder columbusOrder)
        {
            return new Case
            {
                CreatedDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(columbusOrder.creationDate.ToString()) / 1000d)).ToLocalTime(),
                Note = columbusOrder.orderType,
                Id = Convert.ToString(columbusOrder.orderNumber),
                Type = CaseType.CU,
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
