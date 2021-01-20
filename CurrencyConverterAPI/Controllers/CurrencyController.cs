using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Interfaces;
using CurrencyConverterAPI.Data.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrencyConverterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyConverterContext _context;

        private readonly ICurrencyConverterService _currencyConverterService;
        private readonly IRequestLoggerService _requestLoggerService;

        public CurrencyController(ICurrencyConverterService currencyConverterService, IRequestLoggerService requestLoggerService, 
            CurrencyConverterContext context)
        {
            _currencyConverterService = currencyConverterService;
            _requestLoggerService = requestLoggerService;
            _context = context;
        }

        /// <summary>
        /// Get all available currencies.
        /// </summary>
        /// <remarks>
        /// Returns list of string with available currency ISO 4217 codes.
        /// </remarks>
        [HttpGet("AvailableCurrencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetAvailableCurrencies()
        {
            
            var result = _currencyConverterService.GetAvailableCurrencies();
            await _requestLoggerService.LogRequest("/AvailableCurrencies", false, _context);

            return Ok(JsonConvert.SerializeObject(result)); 

        }

        /// <summary>
        /// Recount value from one currency to another
        /// </summary>
        /// <remarks>
        /// Returns a conversion result of specified amount from one currency to another based on current currency rates.
        /// Example: Recount/100/USD/EUR  returns 121.18 
        ///     what means: 100 USD  =  121.18 EUR
        /// </remarks>
        /// <param name="amount">Value to convert - bigger than zero, if has decimal part - only after dot</param>
        /// <param name="fromCurrency">ISO Code of 'From' currency, case insensitive </param>
        /// <param name="toCurrency">ISO Code of 'To' currency, case insensitive </param>
        [HttpGet]
        [Route("Recount/{amount}/{fromCurrency}/{toCurrency}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<string>> RecountAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            fromCurrency.ToUpper();
            toCurrency.ToLower();
            var isApiLogged = await _requestLoggerService.LogRequest($"/Recount/{amount}/{fromCurrency}/{toCurrency}", false, _context);

            if (amount > 0 &&_currencyConverterService.IsCurrencyValid(fromCurrency) && _currencyConverterService.IsCurrencyValid(toCurrency))
            {
                var result = await _currencyConverterService.RecountCurrency(amount, fromCurrency, toCurrency);

                var isFromCurrencyLogged = await _requestLoggerService.LogRequest($"/{fromCurrency}", true, _context);
                var isToCurrencyLogged = await _requestLoggerService.LogRequest($"/{toCurrency}", true, _context);

                if (result != -1 && isApiLogged && isFromCurrencyLogged && isToCurrencyLogged)
                {
                    return Ok(JsonConvert.SerializeObject(result));
                }
                else return StatusCode(500);
            }

            return BadRequest("Invalid parameters");
        }

        /// <summary>
        /// Get rates of available currencies.
        /// </summary>
        /// <remarks>
        /// Returns list of objects containing pairs - currencyCode : currencyRate.
        /// </remarks>
        [HttpGet]
        [Route("AvailableCurrencyRates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetAvaliableCurrencyRates()
        {
            var result = await _currencyConverterService.GetAvailableCurrencyRates();
            var isLogged = await _requestLoggerService.LogRequest("/AvailableCurrencyRates", false, _context);
            if (isLogged)
            {
                return Ok(JsonConvert.SerializeObject(result));
            }
            else return StatusCode(500);
        }
    }
}
