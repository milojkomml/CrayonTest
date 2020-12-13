using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrayonTest.Models
{
    public class ExchangeRateResponse
    {
        public float MaxExchangeRate { get; set; }
        public float MinExchangeRate { get; set; }
        public float AverageExchangeRate { get; set; }
        public string MaxExchangeRateDate { get; set; }
        public string MinExchangeRateDate { get; set; }
    }
}
