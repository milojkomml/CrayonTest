using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrayonTest.Models;

namespace CrayonTest.Services.Interface
{
    public interface IExchangeRateService
    {
        public DatePeriod GetDatePeriod(List<string> listOfDates);
        public ExchangeRateResponse GetData(Dictionary<string, Rates> rates, string targetCurrency);
    }
}
