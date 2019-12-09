using System;

namespace TC.Services.Models
{
    public abstract class Tariff
    {
        public int Id { get; set; }

        /// <summary>
        /// Tariff name
        /// </summary>
        public string Name { get; set; }

        /// <summary>Annual costs calculation based on the annual consumption</summary>
        /// <param name="annualConsumption">Consumption (kWh/year)</param>
        /// <returns>Annual cost in euros</returns>
        public abstract decimal CalculateAnnualCosts(decimal annualConsumption);
    }
}