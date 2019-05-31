using System;
using TPCIP.CustomerClosedCases.DataModel;

namespace TPCIP.CustomerClosedCases.Fakes
{
    public class EtrayAgent : IEtrayAgent
    {
        public virtual ETrayDocument[] GetETrayClosedCases(string lid)
        {
            return new[] 
                { 
                    new ETrayDocument { created = DateTime.Now, docId = "*eTray Archieved 1", linkUrl = "http://accenture.com/" },
                    new ETrayDocument { created = DateTime.Now, docId = "*eTray Archieved 2", linkUrl = "http://accenture.com/" },
                    new ETrayDocument { created = DateTime.Now, docId = "*eTray Archieved 3", linkUrl = "http://accenture.com/" },
                };
        }
    }
}
