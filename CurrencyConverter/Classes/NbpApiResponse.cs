using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Classes.Dto
{
    public class NbpApiResponse
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public IList<Rate> Rates { get; set; }

        public class Rate
        {
            public string No { get; set; }
            public DateTime EffectiveDate { get; set; }
            public decimal Mid { get; set; }

        }
    }
    


}
