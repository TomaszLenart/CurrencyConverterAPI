using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CurrencyConverter.DataAccess.Models
{
    public class RequestLog
    {
        [Key]
        [Required]
        public int LogId { get; set; }
        public DateTime Date { get; set; }
        public string Request { get; set; }
    }
}
