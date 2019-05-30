using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPCIP.ActiveUserGuides.DataModel;
using TPCIP.ActiveUserGuides.FakeData;

namespace TPCIP.ActiveUserGuides.Fakes
{

    public class CustomerAgent : ICustomerAgent
    {
        private const string CUSTOMER_ID_TEST = "64715008";

        public static string[] SampleSectionNames = { "*Fake*Mobil", "*Fake* Broadband", "*Fake*Mobil" };
        public static string[] SampleEntityTitles = { "*Fake* Broadband Hotline", "*Fake*Mobil", "*Fake*Mobil" };
        public static string[] SampleEntityNames = { "Broadband Hotline", "Mobil", "Mobil" };
        public static string[] SampleEntityTypes = { "ArticleNote", "ArticleNote", "GuideNote", "ArticleNote", "GuideNote" };
        public static string[] SampleUserInitials = { "DER", "BC", "" };

        public virtual List<CustomerNote> getCustomerNotesByUserId(string userId, int page, int pageSize, string status)
        {
            var result = _GetNotesMock(CUSTOMER_ID_TEST, "2452", page, pageSize, status).Take(pageSize).ToList();
            foreach (var customerNote in result)
            {
                customerNote.userName = userId;
            }
            return result;
        }

        private List<CustomerNote> _GetNotesMock(string customerId, string accountNo, int page, int pageSize, string status)
        {
            List<CustomerNote> result;

            var random = new Random(10);
            int j = 0;
            result = Enumerable.Range(0, 18).Select(
                num =>
                {
                    var randNumber = random.Next(1, 2);
                    return new CustomerNote
                    {
                        entityId = GetEntityId(num),
                        //sectionName = num == 1 || num == 6 || num == 11 || num == 16 ? string.Format("{0} {1}", "*Fake CCF TeleMarketing", num) : string.Format("{0} {1}", SampleSectionNames.GetValue(randNumber), num),
                        sectionName = num == 1 || num == 6 || num == 11 || num == 16 ? string.Format("{0}", "1") : string.Format("{0} {1}", SampleSectionNames.GetValue(randNumber), num),
                        entityTitle = string.Format("{0} {1}", SampleEntityTitles.GetValue(randNumber), num),
                        entityName = string.Format("{0} {1}", SampleEntityNames.GetValue(randNumber), num),
                        entityType = (string)SampleEntityTypes.GetValue(randNumber),
                        userInitials = SampleUserInitials.GetValue(randNumber).ToString(),
                        additionalValues = j++ % 2 == 0 ?
                        new List<AdditionalValue>()
                        {   
                            new AdditionalValue(){ key = "resumable",value = "false"},
                            new AdditionalValue(){ key = "ActualLidId",value = FakeCustomerIds.Customer01},
                            new AdditionalValue(){ key = "SystemNote",value = "Textsdfsdfsdfsdfsdfsdfsdff fsdfsdfsd sdffd1 @@@ Text2@@@"},
                        } :
                        new List<AdditionalValue>() 
                        {   
                            new AdditionalValue() { key = "resumable", value = "true" }, 
                            new AdditionalValue() { key = "ActualLidId", value = FakeCustomerIds.Customer01 },
                            new AdditionalValue(){ key = "SystemNote",value = "Textsdfsdfsdfsdfsdfsdfsdff fsdfsdfsd sdffd1 @@@ Text2@@@"},
                        },
                    };

                }).Skip(page * pageSize).Take(pageSize).ToList();

            var i = 0;
            foreach (var note in result)
            {
                note.id = 401 + i;
                note.lid = customerId;
                note.customerName = customerId == CUSTOMER_ID_TEST ? "**Daniel Turan" : "**Josef Mrkvicka";
                note.userInitials = customerId == CUSTOMER_ID_TEST ? "**DT" : "**JM";
                note.note = "First LineSecond Linehird LineFourth LineFifth Line Sixth Line Seventh Line Eighth Line Ninth Line Tenth Line Eleventh and so on" + page;
                note.created = new DateTime(2013, 7, 09, 13, 45, 0).AddDays(-i - page).ToString("yyyy-MM-ddTHH:mm:ss");
                note.lastUpdated = new DateTime(2013, 8, 09, 13, 45, 0).AddDays(-i - page).ToString("yyyy-MM-ddTHH:mm:ss");
                note.userName = "testpersonen";
                note.userId = (i == 0 || i == 1) ? "m57943" : "m32321";
                note.entityTitle = "*Fake* Broadband Hotline";

                note.status = string.IsNullOrEmpty(status) ? i % 2 == 0 ? "Parked" : "Ended" : status;
                if (i == 0)
                {
                    note.systemName = "";
                }
                else
                {
                    note.systemName = i % 2 == 0 ? "TP" : "CIP";
                }

                if (note.entityType == "GuideNote")
                {
                    note.entityStep = "**Step";
                }
                i++;
            }
            return result;
        }

        public static string GetEntityId(int i)
        {
            return new Guid(i, 0, 0, new byte[8]).ToString();
        }
    }
}
