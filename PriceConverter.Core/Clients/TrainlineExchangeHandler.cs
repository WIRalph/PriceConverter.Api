using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PriceConverter.Core.Models.Response;

namespace PriceConverter.Core.Clients
{
    public class TrainlineExchangeHandler : ITrainlineExchangeHandler
    {
        private readonly IHttpClientFactory _httpClient;
        public TrainlineExchangeHandler(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CurrencyExchangeRateResponse> GetRates(string currency)
        {
            var urlPath = $"/exchangerates/api/latest/{currency}.json";
            var request = CreateGetRequest(urlPath);
            using var client = CreateHttpClient();
            var response = await client.SendAsync(request);
            return await ReadResponse<CurrencyExchangeRateResponse>(response);
        }
        
        private static string BaseRequestUrl()
        {
            return $"https://trainlinerecruitment.github.io";
        }
        private HttpRequestMessage CreateGetRequest(string urlPath)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, urlPath);
            return httpRequest;
        }
        
        private HttpClient CreateHttpClient()
        {
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(BaseRequestUrl());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        
        private async Task<T> ReadResponse<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException($"Error: could not read response currency from {BaseRequestUrl()}");
            return responseMessage.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync())
                : default;
        }
    }
}