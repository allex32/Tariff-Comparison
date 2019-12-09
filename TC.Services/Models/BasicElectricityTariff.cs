using System;

namespace TC.Services.Models
{
    public class BasicElectricityTariff : Tariff
    {
        /// <summary>
        /// Base cost per month (in euros)
        /// </summary>
        public decimal MonthlyTariffCost { get; set; }

        /// <summary>
        /// Consumption cost per kWh (in euros)
        /// </summary>
        public decimal ConsumptionCost { get; set; }

        public override decimal CalculateAnnualCosts(decimal annualConsumption)
        {
            if (MonthlyTariffCost < 0 || ConsumptionCost < 0)
            {
                throw new InvalidOperationException(
                    $"{nameof(MonthlyTariffCost)} " +
                    $"or {nameof(ConsumptionCost)} " +
                    $"parameters can not be less than zero");
            }

            if (annualConsumption < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(annualConsumption)} parameter must be greater than or equal to zero");
            }

            return this.MonthlyTariffCost * 12 + this.ConsumptionCost * annualConsumption;
        }
    }
}