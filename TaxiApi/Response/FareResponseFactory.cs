using Newtonsoft.Json;
using TaxiApi.Request;

namespace TaxiApi.Response
{
    public interface ICreateTaxiFareResponses
    {
        FareResponse Create(string fareRequest);
    }

    public class TaxiFareResponse : ICreateTaxiFareResponses
    {
        private readonly IPerformApiRequest _performApiRequest;

        public TaxiFareResponse(IPerformApiRequest performApiRequest)
        {
            _performApiRequest = performApiRequest;
        }

        public TaxiFareResponse()
        {
            _performApiRequest = new WebClientApiRequest();
        }

        public FareResponse Create(string fareRequest)
        {
            var response = _performApiRequest.Perform(fareRequest);
            return DeserializeResponse(response);
        }

        private static FareResponse DeserializeResponse(string response)
        {
            return JsonConvert.DeserializeObject<FareResponse>(response);
        }
    }
}