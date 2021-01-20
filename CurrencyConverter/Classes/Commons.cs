using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Classes
{
    public class Commons
    {
        public static List<string> Currencies = new List<string> { "USD", "EUR", "CHF", "JPY", "AUD" };
        public static string API_ROUTE = "http://localhost:4200/currency";
        public static string NBP_ROUTE = "http://api.nbp.pl/api/exchangerates/rates/a";

    }
}
