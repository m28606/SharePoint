using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.PortalUserGuides.Domain;
using TPCIP.ServiceFacade.Mapping;
using System.Globalization;
using TPCIP.ServiceFacade.Internal;
using TPCIP.ToolBox.User;

namespace TPCIP.PortalUserGuides
{
    public class GuideFacade
    {
        private readonly ICustomerAgent _customerAgent;
        private readonly ISettings _settings;
        private const int DefaultPageSize = 5;

        public GuideFacade(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }

            _customerAgent = serviceLocator.GetService<ICustomerAgent>();

            _settings = serviceLocator.Settings;
        }

        public List<GuideSessionHistory> GetGuidesForPortalUser(string portalUserId, int pageIndex, CustomerNoteStatus status, int pageSize = DefaultPageSize)
        {
            if (string.IsNullOrEmpty(portalUserId)) throw new ArgumentNullException("portalUserId");

            //bc call
            var notes = _customerAgent.getCustomerNotesByUserId(portalUserId, pageIndex, pageSize, status.ToString());

            //mapping
            var result = notes.Select(CustomerNoteMapper.MapGuideSessionHistory).ToList();

            return result;
        }

        public void EndGuide(long guidenoteId, string portalUserId, string session, string noteText, string stepId, string departmentName, string[] additionalValues = null)
        {
            UpdateNote(
                    id: guidenoteId,
                    portalUserId: portalUserId,
                    noteText: noteText,
                    additionalValues: additionalValues,
                    departmentName: departmentName,
                    status: CustomerNoteStatus.Ended,
                    sessionId: EncodingFormatter.FromDanishToUtf(session),
                    stepId: stepId);
        }

        private DataModel.CustomerNote UpdateNote(long id, string portalUserId, string noteText, string[] additionalValues, CustomerNoteStatus status, string sessionId, string stepId, string departmentName)
        {
            var customerNote = _customerAgent.getCustomerNote(id.ToString(CultureInfo.InvariantCulture));
            if (customerNote == null) throw new InvalidOperationException("CustomerAgent.getCustomerNote returned null for id=" + id);
            customerNote.address = CustomerNoteMapper.EnsureNotEmptyString(customerNote.address);
            customerNote.zip = CustomerNoteMapper.EnsureNotEmptyString(customerNote.zip);
            customerNote.city = CustomerNoteMapper.EnsureNotEmptyString(customerNote.city);
            customerNote.additionalValues = CustomerNoteMapper.MapAdditionalValues(customerNote.additionalValues, additionalValues);
            customerNote.departmentName = departmentName;

            if (portalUserId != null)
            {
                customerNote.userId = portalUserId;
                customerNote.userName = CustomerNoteMapper.EnsureNotEmptyString(UserToolBox.GetFullUserName());
                customerNote.userInitials = CustomerNoteMapper.EnsureNotEmptyString(UserToolBox.GetUserNameInitials());
                customerNote.departmentName = departmentName;
            }

            if (noteText != null)
            {
                customerNote.note = CustomerNoteMapper.EnsureNotEmptyString(noteText);
            }

            customerNote.status = status.ToString();

            if (sessionId != null)
            {
                customerNote.entityId = sessionId;
            }
            if (stepId != null)
            {
                customerNote.entityStep = stepId;
            }

            if (String.IsNullOrEmpty(customerNote.customerBan) || customerNote.customerBan == "0")
            {
                customerNote.customerBan = customerNote.lid.StartsWith("6") && customerNote.lid.Length == 9 ? customerNote.lid : "0";
            }

            //Defect 13791 : Random Article save and close issue 
            if (string.IsNullOrEmpty(customerNote.note)) customerNote.note = " ";//note null or empty not allowed

            //check if either tdcNoteId or youSeeNoteId exists
            if ((customerNote.tdcNoteId != 0) || (customerNote.youseeNoteId != null && customerNote.youseeNoteId != ""))
                _customerAgent.updateCustomerNote(customerNote);

            return customerNote;
        }

    }
}
