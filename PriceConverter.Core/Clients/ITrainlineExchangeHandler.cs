using System.Threading.Tasks;
using PriceConverter.Core.Models.Response;

namespace PriceConverter.Core.Clients
{
    public interface ITrainlineExchangeHandler
    {
        Task<CurrencyExchangeRateResponse> GetRates(string currency);
    }
}