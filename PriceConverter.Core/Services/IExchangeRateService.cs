using System.Threading.Tasks;
using PriceConverter.Core.Models.Requests;

namespace PriceConverter.Core.Services
{
    public interface IExchangeRateService
    {
        Task<decimal> CalculateCurrentPrice(PriceConversionRequest priceRequest);
    }
}