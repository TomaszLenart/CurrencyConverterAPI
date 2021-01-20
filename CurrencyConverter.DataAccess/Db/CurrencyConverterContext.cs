using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverterAPI.Data.Db
{
    public class CurrencyConverterContext : DbContext
    {
        public CurrencyConverterContext(DbContextOptions options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder
             modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Map Entity names to DB Table names
            modelBuilder.Entity<RequestLog>().ToTable("RequestLogs");
        }

        public DbSet<RequestLog> RequestLogs { get; set; }

    }
}
