using System;
using Configuration;
using WebResponse;

namespace TaxiFirmDetails
{
    public interface IConstructGoogleTextSearchRequests
    {
        string GetTextSearchRequests(string latlong);
    }

    public class GoogleTextSearchRequestConstructor : IConstructGoogleTextSearchRequests
    {
        private readonly ICanReadConfigurations _configReader;
        private readonly IDownloadResponses _webClientWrapper;

        public GoogleTextSearchRequestConstructor(ICanReadConfigurations configReader, IDownloadResponses webClientWrapper)
        {
            _configReader = configReader;
            _webClientWrapper = webClientWrapper;
        }

        public string GetTextSearchRequests(string latLong)
        {
            string baseUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
            string location = "location=" + latLong;
            string key = "key=" + _configReader.GooglePlacesApiKey();
            string radius = "radius=100";
            string query = "query=taxi";
            string sensor = "sensor=true";

            var address = String.Format("{0}?{1}&{2}&{3}&{4}&{5}", baseUri, location, radius, query, sensor, key);

            return _webClientWrapper.Get(address);

        }
    }
}