using System;
using TC.Services.Models;
using Xunit;

namespace TC.Tests.CalculationTests
{
    public class PackagedElectricityTariffTests
    {
        [Theory]
        [InlineData(800, 4000, 0.3, 0, 800)]
        [InlineData(800, 4000, 0.3, 3500, 800)]
        [InlineData(800, 4000, 0.3, 4500, 950)]
        [InlineData(800, 4000, 0.3, 6000, 1400)]
        public void IsAnnualCostCalculatedCorrectly(decimal annualTariffCost, decimal annualTariffAmount,
            decimal extraConsumptionCost, decimal annualConsumption, decimal expectedAnnualCost)
        {
            var tariff = new PackagedElectricityTariff()
            {
                Name = string.Empty,
                AnnualTariffCost = annualTariffCost,
                AnnualTariffAmount = annualTariffAmount,
                ExtraConsumptionCost = extraConsumptionCost
            };

            var actualAnnualCost = tariff.CalculateAnnualCosts(annualConsumption);

            Assert.Equal(expectedAnnualCost, actualAnnualCost);
        }

        [Theory]
        [InlineData(-1, 1, 1)]
        [InlineData(1, -1, 1)]
        [InlineData(1, 1, -1)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        public void InvalidOperationExceptionShouldBeThrown(decimal annualTariffCost, decimal annualTariffAmount,
            decimal extraConsumptionCost)
        {
            var tariff = new PackagedElectricityTariff()
            {
                Name = string.Empty,
                AnnualTariffCost = annualTariffCost,
                AnnualTariffAmount = annualTariffAmount,
                ExtraConsumptionCost = extraConsumptionCost
            };

            Assert.Throws<InvalidOperationException>(() => tariff.CalculateAnnualCosts(1));
        }

        [Theory]
        [InlineData(-1)]
        public void ArgumentOutOfRangeExceptionShouldBeThrown(decimal annualConsumption)
        {
            var tariff = new PackagedElectricityTariff()
            {
                Name = string.Empty,
                AnnualTariffCost = 1,
                AnnualTariffAmount = 1,
                ExtraConsumptionCost = 1
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => tariff.CalculateAnnualCosts(annualConsumption));
        }
    }
}