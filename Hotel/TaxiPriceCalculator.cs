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
        private readonly ICreateRequests _fareRequestFactory;
        private readonly ICreateResponses _fareResponseFactory;

        public TaxiFareCalculator(ICreateRequests fareRequestFactory, ICreateResponses fareResponseFactory)
        {
            _fareRequestFactory = fareRequestFactory;
            _fareResponseFactory = fareResponseFactory;
        }

        public TaxiFareCalculator()
        {
            _fareRequestFactory = new FareRequestFactory();
            _fareResponseFactory = new FareResponseFactory();
        }

        public double GetTaxiPrice(string distance, string fromLatLong)
        {
            var request = _fareRequestFactory.Create(DateTime.Now, distance, fromLatLong);
            var fareResponse = _fareResponseFactory.Create(request);

            return (double) fareResponse.Fare.Cost;
        }
    }
}