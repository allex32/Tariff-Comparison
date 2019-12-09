using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TC.Services.Models;

namespace TC.Services.Infrastructure
{
    public static class DbInitializer
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new TariffDbContext(serviceProvider.GetRequiredService<DbContextOptions<TariffDbContext>>()))
            {
                await context.Database.EnsureCreatedAsync();

                if (!(await context.BasicElectricityTariffs.AnyAsync()))
                {
                    await context.BasicElectricityTariffs.AddAsync(new BasicElectricityTariff()
                    {
                        Name = "Basic electricity tariff",
                        MonthlyTariffCost = 5,
                        ConsumptionCost = 0.22m
                    });
                }

                if (!(await context.PackagedElectricityTariffs.AnyAsync()))
                {
                    await context.PackagedElectricityTariffs.AddAsync(new PackagedElectricityTariff()
                    {
                        Name = "Packaged tariff",
                        AnnualTariffAmount = 4000,
                        AnnualTariffCost = 800,
                        ExtraConsumptionCost = 0.3m
                    });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}