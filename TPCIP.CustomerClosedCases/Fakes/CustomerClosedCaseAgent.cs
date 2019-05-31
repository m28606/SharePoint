using System.Collections.Generic;
using TPCIP.CustomerClosedCases.DataModel;

namespace TPCIP.CustomerClosedCases.Fakes
{
    public class CustomerClosedCaseAgent : ICustomerClosedCaseAgent
    {
        public virtual List<ColumbusClosedCase> GetCustomerClosedCase(string lid)
        {
            List<ColumbusClosedCase> listClosedCase = new List<ColumbusClosedCase>();
            ColumbusClosedCase closedCase1 = new ColumbusClosedCase
            {
                cancellationDate = "11-10-2016",
                orderNo = "171715134",
                newLid = "YC123456",
                oldLid = "YC123465",
                transcode = "Trans1",
                orderText = "Order Text1",
                customerCaseText = "customerText1",
                fakDate = "01-10-2016",
                kusagkd = "kusagkd1",
                orderDate = "03-11-2016",
                performedDate = "04,10-2016",
            };

            ColumbusClosedCase closedCase2 = new ColumbusClosedCase
            {
                cancellationDate = "12-10-2016",
                orderNo = "171715135",
                newLid = "YC123457",
                oldLid = "YC123467",
                transcode = "Trans2",
                orderText = "Order Text2",
                customerCaseText = "customerText2",
                fakDate = "02-10-2016",
                kusagkd = "kusagkd2",
                orderDate = "04-11-2016",
                performedDate = "05-10-2016",
            };

            ColumbusClosedCase closedCase3 = new ColumbusClosedCase
            {
                cancellationDate = "13-10-2016",
                orderNo = "171713454",
                newLid = "YC123451",
                oldLid = "YC123462",
                transcode = "Trans3",
                orderText = "Order Text3",
                customerCaseText = "customerText3",
                fakDate = "03-10-2016",
                kusagkd = "kusagkd3",
                orderDate = "07-11-2016",
                performedDate = "08-10-2016",
            };

            ColumbusClosedCase closedCase4 = new ColumbusClosedCase
            {
                cancellationDate = "14-10-2016",
                orderNo = "34534553",
                newLid = "EM115311",
                oldLid = "EM120268",
                transcode = "Trans4",
                orderText = "Order Text4",
                customerCaseText = "customerText4",
                fakDate = "05-10-2016",
                kusagkd = "kusagkd4",
                orderDate = "13-11-2016",
                performedDate = "09-10-2016",
            };

            ColumbusClosedCase closedCase5 = new ColumbusClosedCase
            {
                cancellationDate = "16-10-2016",
                orderNo = "56348234",
                newLid = "YC145456",
                oldLid = "YC126765",
                transcode = "Trans5",
                orderText = "Order Text5",
                customerCaseText = "customerText5",
                fakDate = "07-10-2016",
                kusagkd = "kusagkd7",
                orderDate = "15-11-2016",
                performedDate = "18-10-2016",
            };

            listClosedCase.Add(closedCase1);
            listClosedCase.Add(closedCase2);
            listClosedCase.Add(closedCase3);
            listClosedCase.Add(closedCase4);
            listClosedCase.Add(closedCase5);
            return listClosedCase;
        }
    }
}
