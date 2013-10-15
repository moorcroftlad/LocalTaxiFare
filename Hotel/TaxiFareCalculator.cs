using System;
using TaxiApi.Request;
using TaxiApi.Response;

namespace Results
{
    public interface ICalculateTheTaxiFare
    {
        double GetTaxiPrice(string distance, string fromLatLong);
    }

    public class TaxiFareCalculator : ICalculateTheTaxiFare
    {
        private readonly ICreateTaxiFareRequests _fareTaxiFareRequestFactory;
        private readonly ICreateTaxiFareResponses _fareTaxiFareResponseFactory;

        public TaxiFareCalculator(ICreateTaxiFareRequests fareTaxiFareRequestFactory, ICreateTaxiFareResponses fareTaxiFareResponseFactory)
        {
            _fareTaxiFareRequestFactory = fareTaxiFareRequestFactory;
            _fareTaxiFareResponseFactory = fareTaxiFareResponseFactory;
        }

        public TaxiFareCalculator()
        {
            _fareTaxiFareRequestFactory = new TaxiFareRequest();
            _fareTaxiFareResponseFactory = new TaxiFareResponse();
        }

        public double GetTaxiPrice(string distance, string fromLatLong)
        {
            var request = _fareTaxiFareRequestFactory.Create(DateTime.Now, distance, fromLatLong);
            var fareResponse = _fareTaxiFareResponseFactory.Create(request);
            return (double) fareResponse.Fare.Cost;
        }
    }

    public class FakeTaxiFareCalculator : ICalculateTheTaxiFare
    {
        public double GetTaxiPrice(string distance, string fromLatLong)
        {
            return 32.25;
        }
    }
}