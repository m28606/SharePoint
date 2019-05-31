using System;
using TPCIP.CustomerOpenCases.DataModel;

namespace TPCIP.CustomerOpenCases.Fakes
{
    public class BierAgent : IBierAgent
    {
        public virtual BierDocument[] GetBierTt(BierParameter param)
        {
            Random rdm = new Random();
            if (rdm.Next(0, 10) > 5)
            {
                return new[] 
                { 
                    new BierDocument { tid_oprettet = DateTime.Now, id = !string.IsNullOrEmpty(param.kundenummer) ? "*ClosedBierCTT1" : "*ClosedBierITT1" , status="lukket"}, 
                    new BierDocument { tid_oprettet = DateTime.Now, id = !string.IsNullOrEmpty(param.kundenummer) ? "*ClosedBierCTT2" : "*ClosedBierITT2" , status="lukket"}, 
                    new BierDocument { tid_oprettet = DateTime.Now, id = !string.IsNullOrEmpty(param.kundenummer) ? "*ClosedBierCTT3" : "*ClosedBierITT3" , status="lukket"}, 
                
                    new BierDocument { tid_oprettet = DateTime.Now, id = !string.IsNullOrEmpty(param.kundenummer) ? "*OpenBierCTT1" : "*OpenBierITT1" , status="åben"}, 
                    new BierDocument { tid_oprettet = DateTime.Now, id = !string.IsNullOrEmpty(param.kundenummer) ? "*OpenBierCTT2" : "*OpenBierITT2" , status="åben"}, 
                    new BierDocument { tid_oprettet = DateTime.Now, id = !string.IsNullOrEmpty(param.kundenummer) ? "*OpenBierCTT3" : "*OpenBierITT3" , status="åben"}, 
                };
            }          
                throw new Exception("getBierTT service failed");           
        }

        public virtual BierDocument[] GetBierPo(BierParameter param)
        {
            Random rdm = new Random();
            if (rdm.Next(0, 10) > 5)
            {
                return new[] 
                { 
                    new BierDocument { modtaget = DateTime.Now, aktivitet = !string.IsNullOrEmpty(param.kundenummer) ? "*ClosedBierCPO1" : "*ClosedBierIPO1" , bierstatus="lukket"}, 
                    new BierDocument { modtaget = DateTime.Now, aktivitet = !string.IsNullOrEmpty(param.kundenummer) ? "*ClosedBierCPO2" : "*ClosedBierIPO2" , bierstatus="lukket"}, 
                    new BierDocument { modtaget = DateTime.Now, aktivitet = !string.IsNullOrEmpty(param.kundenummer) ? "*ClosedBierCPO3" : "*ClosedBierIPO3" , bierstatus="lukket"}, 
                
                    new BierDocument { modtaget = DateTime.Now, aktivitet = !string.IsNullOrEmpty(param.kundenummer) ? "*OpenBierCPO1" : "*OpenBierIPO1" , bierstatus="åben"}, 
                    new BierDocument { modtaget = DateTime.Now, aktivitet = !string.IsNullOrEmpty(param.kundenummer) ? "*OpenBierCPO2" : "*OpenBierIPO2" , bierstatus="åben"}, 
                    new BierDocument { modtaget = DateTime.Now, aktivitet = !string.IsNullOrEmpty(param.kundenummer) ? "*OpenBierCPO3" : "*OpenBierIPO3" , bierstatus="åben"}, 
                };
            }          
                throw new Exception("getBierPO service failed");           
        }

    }
}
