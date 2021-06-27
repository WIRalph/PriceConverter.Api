using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Models.Response;
using PriceConverter.Core.Services;

namespace PriceConverter.Core.UnitTests
{
    class PriceConverterServiceTests
    {
        private Mock<IExchangeRateService> _exchangeRateServiceMock;
        
        [SetUp]
        public void Setup()
        { 
            _exchangeRateServiceMock = new Mock<IExchangeRateService>();
        }

        [Test]
        public async Task CalculateCurrentPrice_ValidParams_ValidResponse()
        {
            //arrange
            var priceConversionRequest = new PriceConversionRequest
            {
                Price = 1,
                SourceCurrency = "EUR",
                TargetCurrency = "GBP"
            };
            
            var convertedPriceResponse = new ConvertedPriceResponse
            {
                Currency = "GBP",
                CurrentPrice = 1
            };

            _exchangeRateServiceMock.Setup(e =>
                e.CalculateCurrentPrice(priceConversionRequest)).ReturnsAsync(1);
            
            var priceCurrencyConverterService = new PriceConverterService(_exchangeRateServiceMock.Object);
            
            //act
            var response = await priceCurrencyConverterService.ConvertPrice(priceConversionRequest);
            
            //assert
            Assert.AreEqual(convertedPriceResponse.Currency, response.Currency);
        }
    }
}