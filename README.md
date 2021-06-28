# PriceConverter.Api
This is a tech test for a PriceCurrency Converter API

The PriceConverterApi is a POC Web API for Price currency conversion based on latest currency conversion rates.

This service uses Trainline Recruitments exchange service https://trainlinerecruitment.github.io/exchangerates/  to get the rates exchange rates.

The service works via a POST req with a request body i.e 
{
    "price":  1,
    "source_currency": "",
    "target_currency": ""
}

And returns Converted price and target currency.

Possible Future Improvements

* Implement a config map to hold the client url and map the url config at runtime.
* Implement better error handling, maybe write a middleware for this.
* Improve on logging.
* Implement a retry policy for requests made to the client api
* Add further unit tests to improve the covrage
* Implement Integration tests
* Depending on the expected load on the service maybe introduce caching of responses for a small time period.
