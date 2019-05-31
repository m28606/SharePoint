using System;
using TPCIP.ServiceLocatorInterfaces;

namespace TPCIP.CustomerOpenCases
{
    public class CustomerFacade
    {
        public CustomerFacade(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
        }

        public bool IsYouSeeCustomer(string term)
        {
            return term.Length == 9 && term.StartsWith("6");
        }
    }
}
