/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Retrieve and convert latest exchange rates
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.BusinessLogic
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using TrainlineFX.BusinessLogic.Interfaces;
    using TrainlineFX.Configuration;
    using TrainlineFX.Models;

    public class ExchangeRates : IExchangeRates
    {
        private readonly ExchangeRateApiConfig config;

        public ExchangeRates(IOptions<ExchangeRateApiConfig> config)
        {
            this.config = config.Value;
        }

        public async Task<FXRates> RetrieveLatestJsonRates(string sourceCurrency)
        {
            var exchangeUrl = config.ExchangeUrl;
            var queryString = $"{config.BaseCurrency}{sourceCurrency}";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(exchangeUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(queryString).Result;
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<FXRates>(json);
            }
            else
            {
                return null;
            }
        }
    }
}
