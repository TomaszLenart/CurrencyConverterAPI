using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Classes;
using CurrencyConverter.Classes.Dto;
using CurrencyConverter.Interfaces;
using Newtonsoft.Json;

namespace CurrencyConverter.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        public List<string> GetAvailableCurrencies()
        {
            return Commons.Currencies;
        }

        public async Task<decimal> RecountCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            decimal fromRate = await GetCurrencyRateAsync(fromCurrency);
            decimal toRate = await GetCurrencyRateAsync(toCurrency);
            if (fromRate == -1 || toRate == -1)
                return -1;

            decimal result = (amount * fromRate) / toRate;

            return decimal.Round(result, 2);
            
        }

        private async Task<decimal> GetCurrencyRateAsync(string currency)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync($"{Commons.NBP_ROUTE}/{currency}");
            var nbpResponse = JsonConvert.DeserializeObject<NbpApiResponse>(response);
            var rate = nbpResponse.Rates[0]?.mid;
           
            return rate ?? -1;
        }

        public async Task<Dictionary<string,decimal>> GetAvailableCurrencyRates()
        {
            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            var client = new HttpClient();

            foreach (var currency in Commons.Currencies)
            {
                var response = await client.GetStringAsync($"{Commons.NBP_ROUTE}/{currency}");
                var nbpResponse = JsonConvert.DeserializeObject<NbpApiResponse>(response);

                if(nbpResponse.Rates?.Count > 0)
                    result.Add(nbpResponse.code, nbpResponse.Rates[0].mid);
            }
            return result;
        }

        public bool IsCurrencyValid(string currency)
        {
            return Commons.Currencies.Contains(currency);
        }
    }
}
