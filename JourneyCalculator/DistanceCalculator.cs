using System;

namespace JourneyCalculator
{
    public class DistanceCalculator : ICanGetTheDistanceOfATaxiJourneyBetweenPoints
    {
        private readonly IGetTheResponseFromGoogleMapsDirectionsApi _googleMapsDirectionsResponse;
        private readonly IDeserialiseGoogleMapsDirectionsResponses _googleMapsApiDeserialiser;

        public DistanceCalculator(IGetTheResponseFromGoogleMapsDirectionsApi googleMapsDirectionsResponse, IDeserialiseGoogleMapsDirectionsResponses googleMapsApiDeserialiser)
        {
            _googleMapsDirectionsResponse = googleMapsDirectionsResponse;
            _googleMapsApiDeserialiser = googleMapsApiDeserialiser;
        }

        public DistanceCalculator()
        {
            _googleMapsDirectionsResponse = new GoogleMapsDirectionsResponse();
            _googleMapsApiDeserialiser = new GoogleMapsApiDeserialiser();
        }

        public string Calculate(string from, string to)
        {
            var response = _googleMapsDirectionsResponse.Generate(from, to);
            var googleMapsDirections = _googleMapsApiDeserialiser.DeserializeResponse(response);
            return DistanceInMetres(googleMapsDirections);
        }

        private static string DistanceInMetres(DirectionsResponse directionsResponse)
        {
            Routes firstRoute = directionsResponse.Routes[0];
            Legs firstLeg = firstRoute.Legs[0];
            Distance legDistance = firstLeg.Distance;
            string distanceInMetres = legDistance.Value;
            return distanceInMetres;
        }
    }
}