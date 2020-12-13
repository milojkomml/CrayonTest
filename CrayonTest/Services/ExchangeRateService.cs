using CrayonTest.Models;
using CrayonTest.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrayonTest.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        public DatePeriod GetDatePeriod(List<string> listOfDates)
        {
            var response = new DatePeriod();
            List<DateTime> dates = new List<DateTime>();
            DateTime startDate;
            DateTime endDate;

            //convert from string to DateTime
            foreach (var date in listOfDates)
            {
                var dateParse = DateTime.Parse(date);

                dates.Add(dateParse);
            }

            //set first values
            startDate = dates[0];
            endDate = dates[0];

            foreach (var dateD in dates)
            {
                var dateCompareStart = DateTime.Compare(dateD, startDate);
                var dateCompareEnd = DateTime.Compare(dateD, endDate);

                if(dateCompareStart < 0)
                {
                    startDate = dateD;
                }
                
                if(dateCompareEnd > 0)
                {
                    endDate = dateD.AddDays(1);
                }
            }

            response.startDate = startDate.ToString("yyyy-MM-dd");
            response.endDate = endDate.ToString("yyyy-MM-dd");

            return response;
        }

        public ExchangeRateResponse GetData(Dictionary<string, Rates> rates, string targetCurrency)
        {
            var response = new ExchangeRateResponse();
            float maxRate = 0;
            float minRate = float.MaxValue;
            float averageRate = 0;
            string maxRateDate = "";
            string minRateDate = "";

            float sum = 0;

            foreach (var y in rates)
            {
                Dictionary<string, float> h = new Dictionary<string, float>();

                h.Add("CAD", y.Value.CAD);
                h.Add("HKD", y.Value.HKD);
                h.Add("ISK", y.Value.ISK);
                h.Add("PHP", y.Value.PHP);
                h.Add("DKK", y.Value.DKK);
                h.Add("HUF", y.Value.HUF);
                h.Add("CZK", y.Value.CZK);
                h.Add("AUD", y.Value.AUD);
                h.Add("RON", y.Value.RON);
                h.Add("SEK", y.Value.SEK);
                h.Add("IDR", y.Value.IDR);
                h.Add("INR", y.Value.INR);
                h.Add("BRL", y.Value.BRL);
                h.Add("RUB", y.Value.RUB);
                h.Add("HRK", y.Value.HRK);
                h.Add("JPY", y.Value.JPY);
                h.Add("THB", y.Value.THB);
                h.Add("CHF", y.Value.CHF);
                h.Add("SGD", y.Value.SGD);
                h.Add("PLN", y.Value.PLN);
                h.Add("BGN", y.Value.BGN);
                h.Add("TRY", y.Value.TRY);
                h.Add("CNY", y.Value.CNY);
                h.Add("NOK", y.Value.NOK);
                h.Add("NZD", y.Value.NZD);
                h.Add("ZAR", y.Value.ZAR);
                h.Add("USD", y.Value.USD);
                h.Add("MXN", y.Value.MXN);
                h.Add("ILS", y.Value.ILS);
                h.Add("GBP", y.Value.GBP);
                h.Add("KRW", y.Value.KRW);
                h.Add("MYR", y.Value.MYR);
                h.Add("EUR", y.Value.EUR);

                //float helper = float.Parse(y.GetType().GetProperty(targetCurrency).Name;

                float helper = h.FirstOrDefault(x => x.Key == targetCurrency).Value;

                if (helper > maxRate)
                {
                    maxRate = helper;
                    maxRateDate = y.Key;
                }
                if (helper < minRate)
                {
                    minRate = helper;
                    minRateDate = y.Key;
                }

                sum += helper;
            }

            averageRate = sum / rates.Values.Count();

            response.MaxExchangeRate = maxRate;
            response.MaxExchangeRateDate = maxRateDate;
            response.MinExchangeRate = minRate;
            response.MinExchangeRateDate = minRateDate;
            response.AverageExchangeRate = averageRate;

            return response;
        }
    }
}
