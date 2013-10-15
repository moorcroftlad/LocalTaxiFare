using System;
using System.Collections.Generic;
using WebResponse;

namespace JourneyCalculator
{
    public interface IGetTheResponseFromGoogleMapsDirectionsApi
    {
        string Generate(string from, string to);
    }

    /// <summary>
    /// https://developers.google.com/maps/documentation/directions/#DirectionsResponseElements
    /// </summary>
    public class DirectionsResponse
    {
        public List<Routes> Routes { get; set; }
    }

    public class GoogleMapsDirectionsResponse : IGetTheResponseFromGoogleMapsDirectionsApi
    {
        private readonly IDownloadResponses _webResponseDownloader;

        public GoogleMapsDirectionsResponse(IDownloadResponses webResponseDownloader)
        {
            _webResponseDownloader = webResponseDownloader;
        }

        public GoogleMapsDirectionsResponse()
        {
            _webResponseDownloader = new WebClientWrapper();
        }

        public string Generate(string from, string to)
        {
            const string baseUri = "http://maps.googleapis.com/maps/api/directions/";
            var address = String.Format("{0}json?origin={1}&destination={2}&sensor=false", baseUri, from, to);
            return _webResponseDownloader.Get(address);
        }
    }
}