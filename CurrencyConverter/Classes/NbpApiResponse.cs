using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Classes.Dto
{
    public class NbpApiResponse
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public IList<Rate> Rates { get; set; }

        public class Rate
        {
            public string no { get; set; }
            public DateTime effectiveDate { get; set; }
            public decimal mid { get; set; }

        }
    }
    


}
