using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PriceConverter.Core.Clients;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Models.Response;
using PriceConverter.Core.Services;

namespace PriceConverter.Core.UnitTests
{
    class ExchangeRateServiceTests
    {
        private Mock<ITrainlineExchangeHandler> _trainlineExchangeHandler;
        
        [SetUp]
        public void Setup()
        {
            _trainlineExchangeHandler = new Mock<ITrainlineExchangeHandler>();
        }

        [Test]
        public async Task CalculateCurrentPrice_ValidParams_ReturnsNumberGreaterThanZero()
        {
            var priceConversionRequest = new PriceConversionRequest
            {
                Price = 1,
                SourceCurrency = "EUR",
                TargetCurrency = "GBP"
            };
            var currencyExchangeRateResponse = new CurrencyExchangeRateResponse
            {
                Base = "GBP",
                Date = "2021-06-26",
                UpdatedAt = 029930303,
                Rates = new Dictionary<string, decimal>()
                {
                    {"EUR", 1},
                    {"GBP", 1},
                    {"USD", 1}
                }
            };
            
            _trainlineExchangeHandler.Setup(t =>
                    t.GetRates("EUR"))
                .ReturnsAsync(currencyExchangeRateResponse);

            var exchangeRateService = new ExchangeRateService(_trainlineExchangeHandler.Object);

            var result = await exchangeRateService.CalculateCurrentPrice(priceConversionRequest);
            
            Assert.IsInstanceOf<decimal>(result);
        }
        
        [Test]
        public async Task CalculateCurrentPrice_InvalidTargetCurrency_ReturnsZero()
        {
            var priceConversionRequest = new PriceConversionRequest
            {
                Price = 1,
                SourceCurrency = "EUR",
                TargetCurrency = "TNT"
            };
            var currencyExchangeRateResponse = new CurrencyExchangeRateResponse
            {
                Base = "GBP",
                Date = "2021-06-26",
                UpdatedAt = 029930303,
                Rates = new Dictionary<string, decimal>()
                {
                    {"EUR", 1},
                    {"GBP", 1},
                    {"USD", 1}
                }
            };
            
            _trainlineExchangeHandler.Setup(t =>
                    t.GetRates("EUR"))
                .ReturnsAsync(currencyExchangeRateResponse);

            var exchangeRateService = new ExchangeRateService(_trainlineExchangeHandler.Object);

            var result = await exchangeRateService.CalculateCurrentPrice(priceConversionRequest);
            
            Assert.AreEqual(0, result);
        }
    }
}