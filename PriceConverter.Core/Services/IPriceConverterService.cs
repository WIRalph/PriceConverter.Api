using System.Threading.Tasks;
using PriceConverter.Core.Models.Requests;
using PriceConverter.Core.Models.Response;

namespace PriceConverter.Core.Services
{
    public interface IPriceConverterService
    {
        Task<ConvertedPriceResponse> ConvertPrice(PriceConversionRequest priceRequest);
    }
}