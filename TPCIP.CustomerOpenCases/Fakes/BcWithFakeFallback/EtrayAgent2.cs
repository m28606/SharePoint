
namespace TPCIP.CustomerOpenCases.Fakes.BcWithFakeFallback
{
    public class EtrayAgent2 : EtrayAgent
    {
        private readonly IEtrayAgent _bcChannel;

        public EtrayAgent2(IEtrayAgent bcChannel)
        {
            _bcChannel = bcChannel;
        }
    }
}
