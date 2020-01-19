namespace TC.WebApi.Models
{
    /// <example>
    /// {
    ///   "tariffName": "Basic electricity tariff",
    ///   "annualCosts": 82,
    ///   "annualConsumption": 100
    /// }
    /// </example>
    public class GetCalculatedTariffModel
    {
        /// <summary>
        /// Tariff name
        /// </summary>
        public string TariffName { get; set; }
        
        /// <summary>
        /// Annual cost in euros
        /// </summary>
        public decimal AnnualCosts { get; set; }
        
        /// <summary>
        /// Consumption (kWh/year)
        /// </summary>
        public decimal AnnualConsumption { get; set; }
    }
}