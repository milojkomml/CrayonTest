using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrayonTest.Models
{
    public class ExchangeRateRequest
    {
        public List<string> ListOfDates { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrnecy { get; set; }
    }
}
