using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Services;

namespace PriceConverter.Api.Controllers
{
    [ApiController]
    [Route("/currencyExchangeConverter")]
    public class PriceConverterController : ControllerBase
    {
        private readonly IPriceConverterService _priceConverterService;

        public PriceConverterController(IPriceConverterService priceConverterService)
        {
            _priceConverterService = priceConverterService;
        }
        
        [HttpPost]
        [Route("convert")]
        public async Task<IActionResult> GetCurrencyRate([FromBody]PriceConversionRequest request)
        {
            if (request == null)
                return BadRequest();
            
            var result = await _priceConverterService.ConvertPrice(request);
            
            if (result == null)
                return NotFound("Price conversion not found");
            return new OkObjectResult(result);
        }
    }
}