using TC.Services.Models;

namespace TC.Services.Services
{
    public interface ITariffService
    {
        Tariff[] GetAllTariffs();
    }
}