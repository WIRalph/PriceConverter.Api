using System;
using System.Linq;
using System.Threading.Tasks;
using PriceConverter.Core.Clients;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Models.Response;

namespace PriceConverter.Core.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly ITrainlineExchangeHandler _trainlineExchangeHandler;

        public ExchangeRateService(ITrainlineExchangeHandler trainlineExchangeHandler)
        {
            _trainlineExchangeHandler = trainlineExchangeHandler;
        }

        public async Task<decimal> CalculateCurrentPrice(PriceConversionRequest priceRequest)
        {
            var currentRates = await _trainlineExchangeHandler.GetRates(priceRequest.SourceCurrency);
            
            var found = currentRates.Rates.TryGetValue(priceRequest.TargetCurrency, out decimal rate);
            if (!found)
                throw new ArgumentNullException("Rate not found");

            var currentPrice = priceRequest.Price * rate;
            return currentPrice;
        }
        
    }
}