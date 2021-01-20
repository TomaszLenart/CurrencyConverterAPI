using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Classes.Dto;

namespace CurrencyConverter.Interfaces
{
    public interface ICurrencyConverterService
    {
        Task<decimal> RecountCurrency(decimal amount, string fromCurrency, string toCurrency);
        Task<Dictionary<string, decimal>> GetAvailableCurrencyRates();
        List<string> GetAvailableCurrencies();

        bool IsCurrencyValid(string currency);


    }
}
