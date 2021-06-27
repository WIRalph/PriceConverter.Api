using System.Collections.Generic;
using Newtonsoft.Json;
using PriceConverter.Core.Models.Constants;

namespace PriceConverter.Core.Models.Response
{
    public class CurrencyExchangeRateResponse
    {
        [JsonProperty(AnnotationConstants.Base)]
        public string Base { get; set; }
        [JsonProperty(AnnotationConstants.Date)]
        public string Date { get; set; }
        [JsonProperty(AnnotationConstants.UpdatedAt)]
        public double UpdatedAt { get; set; }
        [JsonProperty(AnnotationConstants.Rates)]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}