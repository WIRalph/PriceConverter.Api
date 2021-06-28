using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using PriceConverter.Core.Clients;
using PriceConverter.Core.Models.Response;

namespace PriceConverter.Core.UnitTests
{
    class TrainlineExchangeHandlerTests
    {
        string _httpResponse;
        private Mock<HttpMessageHandler> _handlerMock;
        private Mock<IHttpClientFactory> mockHttpClientFactory;
        HttpClient _httpClient;
        
        [SetUp]
        public void Setup()
        {
            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _httpResponse = "{'base':'TEST','date':'2021-01-01','time_last_updated':1617753602,'rates':{'GBP':1,'EUR':1,'USD':1}}";
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(_httpResponse),
                })
                .Verifiable();
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("https://testuri.mock"),
            };
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_httpClient);
        }

        [Test]
        public async Task GetExchangeRate_ValidParam_ReturnsObject()
        {
            //arrange
            TrainlineExchangeHandler trainlineExchangeHandler = new TrainlineExchangeHandler(mockHttpClientFactory.Object);
            
            //act
            var response = await trainlineExchangeHandler.GetRates("GBP");
                
            //assert
            Assert.IsInstanceOf<CurrencyExchangeRateResponse>(response);
        }
        
        [Test]
        public async Task GetExchangeRate_InValidParam_ReturnsNotFound404()
        {
            //arrange
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                })
                .Verifiable();
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("https://testuri.mock"),
            };
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_httpClient);
            
            TrainlineExchangeHandler trainlineExchangeHandler = new TrainlineExchangeHandler(mockHttpClientFactory.Object);
            
            //act
            var response = await trainlineExchangeHandler.GetRates("TGT");
                
            //assert
            Assert.IsNull(response);
        }
    }
}