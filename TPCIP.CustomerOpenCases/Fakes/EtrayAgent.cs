using System;
using System.Linq;
using TPCIP.CustomerOpenCases.DataModel;

namespace TPCIP.CustomerOpenCases.Fakes
{
    public class EtrayAgent : IEtrayAgent
    {
        public ETrayDocument[] GetDocuments(string lid)
        {
            if (lid == "64715008")
            {
                return new[] 
                { 
                    new ETrayDocument { created = DateTime.Now, docId = "*eTray 1", linkUrl = "http://accenture.com/" },
                    new ETrayDocument { created = DateTime.Now, docId = "*eTray 2", linkUrl = "http://accenture.com/" },
                    new ETrayDocument { created = DateTime.Now, docId = "*eTray 3", linkUrl = "http://accenture.com/" },
                };
            }

            return Enumerable.Range(1, 8).Select(x => new ETrayDocument
            {
                created = DateTime.Now.AddDays((x - 1) * -50),
                docId = string.Format("*eTray {0}", x),
                linkUrl = "http://accenture.com/"
            }).ToArray();
        }
    }
}
