using System.Text.Json.Serialization;
using PriceConverter.Core.Models.Constants;

namespace PriceConverter.Core.Models.Response
{
    public class ConvertedPriceResponse
    {
        [JsonPropertyName(AnnotationConstants.CurrentPrice)]
        public decimal CurrentPrice { get; set; }
        [JsonPropertyName(AnnotationConstants.Currency)]
        public string Currency { get; set; }
    }
}