using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.ServiceLocatorInterfaces;
using TPCIP.CompletedGuidesForPortalUser.Domain;

namespace TPCIP.CompletedGuidesForPortalUser
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

        
    }
}
