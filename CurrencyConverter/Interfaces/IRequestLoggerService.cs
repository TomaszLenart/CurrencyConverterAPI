using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverterAPI.Data.Db;

namespace CurrencyConverter.Interfaces
{
    public interface IRequestLoggerService
    {
        Task<bool> LogRequest(string url, bool nbpApi, CurrencyConverterContext context);
    }
}
