using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CrayonTest.Models;
using CrayonTest.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace CrayonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : Controller
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ExchangeRateModel ExchangeRate;
        public ExchageRateListModel ExchangeRateList;
        public string endpoint = "";
        public ExchangeRateController(IExchangeRateService exchangeRateService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _exchangeRateService = exchangeRateService;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            endpoint = _configuration.GetConnectionString("ExchangeRatesApi");
        }

        [HttpPost]
        [Route("GetExcRateInfo")]
        public async Task<IActionResult> GetExcRateInfo(ExchangeRateRequest exchangeRateRequest)
        {
            Dictionary<string, Rates> ratesThatIsNeeded = new Dictionary<string, Rates>(); 
            var datePeriod = _exchangeRateService.GetDatePeriod(exchangeRateRequest.ListOfDates);

            string url = $"{endpoint}history?base={exchangeRateRequest.BaseCurrency}&start_at={datePeriod.startDate}&end_at={datePeriod.endDate}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseJson = await client.SendAsync(request);
            ExchangeRateList = await responseJson.Content.ReadFromJsonAsync<ExchageRateListModel>();

            foreach (var i in ExchangeRateList.rates)
            {
                foreach (var x in exchangeRateRequest.ListOfDates)
                {
                    if (i.Key == x)
                    {
                        ratesThatIsNeeded.Add(i.Key, i.Value);
                    }
                }
            }

            var response = _exchangeRateService.GetData(ratesThatIsNeeded, exchangeRateRequest.TargetCurrnecy);

            //var a = await response.Content.ReadFromJsonAsync<ExchangeRateModel>();

            string resp = $"A min exchange rate of {response.MinExchangeRate} on {response.MinExchangeRateDate}, a max exchange rate of {response.MaxExchangeRate} on {response.MaxExchangeRateDate} and average exhange rate was {response.AverageExchangeRate}";

            return Ok(resp);
        }
    }
}