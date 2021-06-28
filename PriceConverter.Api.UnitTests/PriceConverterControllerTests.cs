using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PriceConverter.Api.Controllers;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Models.Response;
using PriceConverter.Core.Services;

namespace PriceConverter.Api.UnitTests
{
    public class PriceConverterControllerTests
    {
        private Mock<IPriceConverterService> _priceConverterMock;
        private ConvertedPriceResponse _priceResponse;
        private Mock<ILogger<PriceConverterController>> _mockLogger;
        
        [SetUp]
        public void Setup()
        {
            _priceConverterMock = new Mock<IPriceConverterService>();
            _mockLogger = new Mock<ILogger<PriceConverterController>>();
        }

        [Test]
        public async Task  TestRequest_ReturnsValidData_StatusCode200()
        {
            //arrange
            var priceConversionRequest = new PriceConversionRequest {Price = (decimal)5.5, SourceCurrency = "GBP", TargetCurrency = "EUR"};
            var expectedResponse = new ConvertedPriceResponse {Currency = "EUR", CurrentPrice = (decimal)6.428686};
            var _httpContext = new DefaultHttpContext();

            _priceConverterMock.Setup(s =>
                s.ConvertPrice(priceConversionRequest)).ReturnsAsync(expectedResponse);

            var priceConverterController = new PriceConverterController(_priceConverterMock.Object, _mockLogger.Object)
            {
                ControllerContext = new ControllerContext() {
                    HttpContext = _httpContext,
                }
            };
            
            //act
            var result = await priceConverterController.PriceCurrencyConvert(priceConversionRequest);

            //assert
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedResponse, okResult.Value);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        
        [Test]
        public async Task  TestInvalidCurrencyRequest_ReturnsNotFound_StatusCode404()
        {
            //arrange
            var priceConversionRequest = new PriceConversionRequest {Price = (decimal)5.5, SourceCurrency = "TGT", TargetCurrency = "MOP"};
            var expectedResponse = "Currency not found";
            var _httpContext = new DefaultHttpContext();

            _priceConverterMock.Setup(s =>
                s.ConvertPrice(priceConversionRequest)).ReturnsAsync(_priceResponse);
            
            var priceConverterController = new PriceConverterController(_priceConverterMock.Object, _mockLogger.Object)
            {
                ControllerContext = new ControllerContext() {
                    HttpContext = _httpContext,
                }
            };
            
            //act
            var result = await priceConverterController.PriceCurrencyConvert(priceConversionRequest);

            //assert
            var okResult = result as NotFoundObjectResult;
            Assert.AreEqual(expectedResponse, okResult.Value);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}