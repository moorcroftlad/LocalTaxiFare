using System;
using Configuration;
using WebResponse;

namespace TaxiFirmDetails
{
    public interface IConstructGooglePlaceRequests
    {
        string GetPlaceRequest(string placeReference);
    }

    public class GooglePlaceRequestConstructor : IConstructGooglePlaceRequests
    {
        private readonly IReadConfiguration _configReader;
        private readonly IDownloadResponses _webClientWrapper;

        public GooglePlaceRequestConstructor(IReadConfiguration configReader, IDownloadResponses webClientWrapper)
        {
            _webClientWrapper = webClientWrapper;
            _configReader = configReader;
        }

        public string GetPlaceRequest(string placeReference)
        {
            string baseUri = "https://maps.googleapis.com/maps/api/place/details/json";
            string sensor = "sensor=true";
            string key = "key=" + _configReader.GooglePlacesApiKey();
            string reference = "reference=" + placeReference;
            var placeRequest = String.Format("{0}?{1}&{2}&{3}", baseUri, reference, sensor, key);
            return _webClientWrapper.Get(placeRequest);
        }
    }
}