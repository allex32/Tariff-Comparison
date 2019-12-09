using Microsoft.EntityFrameworkCore;
using TC.Services.Models;

namespace TC.Services.Infrastructure
{
    public class TariffDbContext : DbContext
    {
        public TariffDbContext(DbContextOptions<TariffDbContext> options)
            : base(options)
        {
        }

        public DbSet<BasicElectricityTariff> BasicElectricityTariffs { get; set; }
        public DbSet<PackagedElectricityTariff> PackagedElectricityTariffs { get; set; }

        /// <remarks>
        /// See https://docs.microsoft.com/ru-ru/ef/core/providers/sqlite/limitations
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BasicElectricityTariff>()
                .Property(e => e.ConsumptionCost)
                .HasConversion<double>();
        }
    }
}