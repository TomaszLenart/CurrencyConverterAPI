using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Classes;
using CurrencyConverter.DataAccess.Models;
using CurrencyConverter.Interfaces;
using CurrencyConverterAPI.Data.Db;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Services
{
    public class RequestLoggerService : IRequestLoggerService 
    {
        public async Task<bool> LogRequest(string url, bool nbpApi, CurrencyConverterContext _context)
        {
            var log = new RequestLog()
            {
                Date = DateTime.Now,
                Request = String.Concat(nbpApi ? Commons.NBP_ROUTE : Commons.API_ROUTE, url)
            };
           
            _context.RequestLogs.Add(log);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;

        }

    }
}
