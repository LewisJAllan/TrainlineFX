/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Controller to get Exchange Rate of Source to Target
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using TrainlineFX.BusinessLogic.Interfaces;
    using TrainlineFX.Models;

    [ApiController]
    [Route("[controller]")]
    public class TrainlineFXController : ControllerBase
    {
        private readonly ICurrencyConverterService currencyConverterService;

        public TrainlineFXController(ICurrencyConverterService currencyConverterService)
        {
            this.currencyConverterService = currencyConverterService ?? throw new ArgumentNullException(nameof(currencyConverterService));
        }

        [HttpGet]
        public IActionResult Get(RequestFX requestFx)
        {
            ResponseFX responseFx = currencyConverterService.ConvertCurrency(requestFx);

            if(responseFx is null)
            {
                return this.BadRequest();
            }

            return this.Ok(responseFx);
        }
    }
}
