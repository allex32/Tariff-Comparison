using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NJsonSchema.Annotations;
using TC.Services.Models;
using TC.Services.Services;
using TC.WebApi.Models;

namespace TC.WebApi.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }
        
        /// <summary>
        /// Get all tariffs with calculated annual costs
        /// </summary>
        /// <param name="consumption">kWh/year</param>
        [HttpGet]
        [Route("compare")]
        public GetCalculatedTariffModel[] Compare([NotNull] decimal consumption)
        {
            var calculatedTariffs = _tariffService.GetAllTariffs().Select(x => ToGetCalculatedTariffModel(x, consumption));
            
            return calculatedTariffs
                .OrderBy(x => x.AnnualCosts)
                .ToArray();
        }

        private GetCalculatedTariffModel ToGetCalculatedTariffModel(Tariff tariff, decimal consumption)
        {
            return new GetCalculatedTariffModel()
            {
                TariffName = tariff.Name,
                AnnualCosts = tariff.CalculateAnnualCosts(consumption),
                AnnualConsumption = consumption
            };
        }
    }
}