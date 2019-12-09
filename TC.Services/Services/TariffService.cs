using System.Linq;
using TC.Services.Infrastructure;
using TC.Services.Models;

namespace TC.Services.Services
{
    public class TariffService : ITariffService
    {
        private readonly TariffDbContext _context;

        public TariffService(TariffDbContext context)
        {
            _context = context;
        }

        public Tariff[] GetAllTariffs()
        {
            var basicTariffs = _context.BasicElectricityTariffs
                .AsEnumerable()
                .Cast<Tariff>();

            var packagedTariffs = _context.PackagedElectricityTariffs
                .AsEnumerable()
                .Cast<Tariff>();

            return basicTariffs
                .Concat(packagedTariffs)
                .ToArray();
        }
    }
}