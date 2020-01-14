
using TC.WebApiInspector.Client;

namespace TC.WebApiInspector.Inspectors
{
    public class TariffInspector : IInspector
    { 
        private readonly TariffClient tariffClient;

        public TariffInspector()
        {
        }

        public void Inspect()
        {
            InspectTariffCompare();
        }

        private void InspectTariffCompare()
        {

        }
    }
}
