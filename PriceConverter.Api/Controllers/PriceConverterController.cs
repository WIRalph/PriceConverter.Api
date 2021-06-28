using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Services;

namespace PriceConverter.Api.Controllers
{
    [ApiController]
    [Route("/priceconverter")]
    public class PriceConverterController : ControllerBase
    {
        private readonly IPriceConverterService _priceConverterService;
        private readonly ILogger<PriceConverterController> _logger;

        public PriceConverterController(IPriceConverterService priceConverterService, ILogger<PriceConverterController> logger)
        {
            _priceConverterService = priceConverterService;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> PriceCurrencyConvert([FromBody]PriceConversionRequest request)
        {
            _logger.LogInformation("Converting price");
            if (request == null)
            {
                _logger.LogWarning("Invalid request");
                return BadRequest();
            }

            var result = await _priceConverterService.ConvertPrice(request);

            if (result == null)
            {
                _logger.LogWarning("Currency exchange not found");
                return NotFound("Currency not found");
            }

            return new OkObjectResult(result);
        }
    }
}