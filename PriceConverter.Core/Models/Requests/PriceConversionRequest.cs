using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PriceConverter.Core.Models.Constants;

namespace PriceConverter.Core.Models.Requests
{
    public class PriceConversionRequest
    {
        [Required]
        [JsonPropertyName(AnnotationConstants.Price)]
        public decimal Price { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [JsonPropertyName(AnnotationConstants.SourceCurrency)]
        public string SourceCurrency { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [JsonPropertyName(AnnotationConstants.TargetCurrency)]
        public string TargetCurrency { get; set;}
    }
}