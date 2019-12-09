using System;

namespace TC.Services.Models
{
    public class PackagedElectricityTariff : Tariff
    {
        /// <summary>
        /// The annual cost of electricity included in the tariff (in euros)
        /// </summary>
        public decimal AnnualTariffCost { get; set; }

        /// <summary>
        /// The annual amount of the electricity included in the tariff (in kWh)
        /// </summary>
        public decimal AnnualTariffAmount { get; set; }

        /// <summary>
        /// Cost per extra kWh (in euros)
        /// </summary>
        public decimal ExtraConsumptionCost { get; set; }

        public override decimal CalculateAnnualCosts(decimal annualConsumption)
        {
            if (AnnualTariffCost <= 0 || AnnualTariffAmount <= 0 || ExtraConsumptionCost <= 0)
                throw new InvalidOperationException(
                    $"{nameof(AnnualTariffCost)} " +
                    $"or {nameof(AnnualTariffAmount)} " +
                    $"or {nameof(ExtraConsumptionCost)} " +
                    $"parameters must be greater than zero");

            if (annualConsumption < 0)
                throw new ArgumentOutOfRangeException(
                    $"{nameof(annualConsumption)} parameter must be greater than or equal to zero");

            var extraConsumptionAmount = annualConsumption > AnnualTariffAmount
                ? annualConsumption - AnnualTariffAmount
                : 0;
            return AnnualTariffCost + ExtraConsumptionCost * extraConsumptionAmount;
        }
    }
}