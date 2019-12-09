using System;
using TC.Services.Models;
using Xunit;

namespace TC.Tests.CalculationTests
{
    public class BasicElectricityTariffTests
    {
        [Theory]
        [InlineData(5, 0.22, 3500, 830)]
        [InlineData(5, 0.22, 4500, 1050)]
        [InlineData(5, 0.22, 6000, 1380)]
        [InlineData(0, 0.22, 6000, 1320)]
        [InlineData(5, 0, 6000, 60)]
        [InlineData(5, 0.22, 0, 60)]
        [InlineData(0, 0, 0, 0)]
        public void AnnualCostShouldBeCalculatedCorrectly(decimal monthlyTariffCost, decimal consumptionCost,
            decimal annualConsumption, decimal expectedAnnualCost)
        {
            var tariff = new BasicElectricityTariff
            {
                Name = string.Empty,
                MonthlyTariffCost = monthlyTariffCost,
                ConsumptionCost = consumptionCost,
            };

            var actualAnnualCost = tariff.CalculateAnnualCosts(annualConsumption);

            Assert.Equal(expectedAnnualCost, actualAnnualCost);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public void InvalidOperationExceptionShouldBeThrown(decimal monthlyTariffCost, decimal consumptionCost)
        {
            var tariff = new BasicElectricityTariff
            {
                Name = string.Empty,
                MonthlyTariffCost = monthlyTariffCost,
                ConsumptionCost = consumptionCost,
            };

            Assert.Throws<InvalidOperationException>(() => tariff.CalculateAnnualCosts(1));
        }

        [Theory]
        [InlineData(-1)]
        public void ArgumentOutOfRangeExceptionShouldBeThrown(decimal annualConsumption)
        {
            var tariff = new BasicElectricityTariff
            {
                Name = string.Empty,
                MonthlyTariffCost = 1,
                ConsumptionCost = 1,
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => tariff.CalculateAnnualCosts(annualConsumption));
        }
    }
}