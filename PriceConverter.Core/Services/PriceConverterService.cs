using System.Threading.Tasks;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Models.Response;

namespace PriceConverter.Core.Services
{
    public class PriceConverterService : IPriceConverterService
    {
        private readonly IExchangeRateService _exchangeRateService;
        public PriceConverterService(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }
        
        public async Task<ConvertedPriceResponse> ConvertPrice(PriceConversionRequest priceRequest)
        {
            var currentPrice = await _exchangeRateService.CalculateCurrentPrice(priceRequest);
            if (currentPrice == 0)
                return null;
            
            var price = new ConvertedPriceResponse
            {
                Currency = priceRequest.TargetCurrency,
                CurrentPrice = currentPrice,
            };
            
            return price;
        }
    }
}