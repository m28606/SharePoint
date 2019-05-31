using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.CompletedGuidesForPortalUser.Domain;
using TPCIP.CompletedGuidesForPortalUser.DataModel;

namespace TPCIP.CompletedGuidesForPortalUser
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
    }
}
