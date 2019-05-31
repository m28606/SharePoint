using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.PortalUserGuides.Domain;
using TPCIP.PortalUserGuides.DataModel;

namespace TPCIP.PortalUserGuides
{
    static public class CustomerNoteMapper
    {
        public static GuideSessionHistory MapGuideSessionHistory(CustomerNote note)
        {
            var result = new GuideSessionHistory
            {
                NoteId = note.id,
                CustomerId = note.lid,
                CustomerName = note.customerName,
                Date = DateTime.Parse(note.lastUpdated ?? note.created),
                EntityId = note.entityName,
                EntityTitle = note.entityTitle,
                StepId = note.entityStep,
                PortalId = note.systemName,
                GuideSessionId = note.entityId,
                Section = note.sectionName,
                ParentAccountNumber = note.customerBan,
                EntityType = note.entityType,
                UserId = note.userId,
                UserName = note.userName,
                NoteText = note.note,
                IsResumable = Convert.ToBoolean(note.additionalValues.FirstOrDefault(av => av.key == "resumable").value),

            };
            return result;
        }

        /// <summary>
        /// customer note cannot accept empty values, so we use space instead of empty string
        /// </summary>
        public static string EnsureNotEmptyString(string domainString)
        {
            if (string.IsNullOrEmpty(domainString)) return " ";
            else
            {
                return domainString;
            }
        }

        public static List<AdditionalValue> MapAdditionalValues(List<AdditionalValue> existingAdditionalValue, string[] newAdditionalValue)
        {
            if (newAdditionalValue != null && newAdditionalValue.Length > 0)
            {

                var systemAdditionalValue = existingAdditionalValue.Where(n => n.key == CustomerNoteAdditionalValue.SystemNote.ToString()).FirstOrDefault();
                var noterMessages = ConvertStringArrayToString(newAdditionalValue, systemAdditionalValue);
                var additionalValue = new AdditionalValue();

                if (systemAdditionalValue != null)
                {
                    systemAdditionalValue.value = noterMessages;
                }
                else
                {
                    additionalValue.key = CustomerNoteAdditionalValue.SystemNote.ToString();
                    additionalValue.value = noterMessages;
                    existingAdditionalValue.Add(additionalValue);
                }

            }
            return existingAdditionalValue;
        }

        static string ConvertStringArrayToString(string[] array, AdditionalValue additionalValue = null)
        {
            StringBuilder noterMessage = new StringBuilder();
            if (additionalValue != null && !string.IsNullOrEmpty(additionalValue.value))
            {
                noterMessage.Append(additionalValue.value);
            }

            foreach (string value in array)
            {
                noterMessage.Append(value);
                noterMessage.Append("@@@");
            }
            //builder.Length -= 1;
            return noterMessage.ToString();
        }
    }
}
