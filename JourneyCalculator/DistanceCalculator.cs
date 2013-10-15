namespace JourneyCalculator
{
    public interface ICalculateDistanceBetweenLatLong
    {
        string Calculate(string from, string to);
    }

    public class DistanceCalculator : ICalculateDistanceBetweenLatLong
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
            var firstRoute = directionsResponse.Routes[0];
            var firstLeg = firstRoute.Legs[0];
            var legDistance = firstLeg.Distance;
            var distanceInMetres = legDistance.Value;
            return distanceInMetres;
        }
    }

    public class FakeDistanceCalculator : ICalculateDistanceBetweenLatLong
    {
        public string Calculate(string @from, string to)
        {
            return "500";
        }
    }
}