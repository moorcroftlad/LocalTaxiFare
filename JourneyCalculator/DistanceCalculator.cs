using System;

namespace JourneyCalculator
{
    public class DistanceCalculator : ICanGetTheDistanceOfATaxiJourneyBetweenPoints
    {
        private readonly IGetTheResponseFromGoogleMapsDirectionsApi _googleMapsDirectionsResponse;
        private readonly IDeserialiseGoogleMapsDirectionsResponses _googleMapsApiDeserialiser;
        private readonly ISpecifyConditionsOfNoTaxiRoutesFound _specifyConditionsOfNoTaxiRoutesFound;

        public DistanceCalculator(IGetTheResponseFromGoogleMapsDirectionsApi googleMapsDirectionsResponse,
                                  IDeserialiseGoogleMapsDirectionsResponses googleMapsApiDeserialiser, ISpecifyConditionsOfNoTaxiRoutesFound noTaxiRoutesSpecification)
        {
            _googleMapsDirectionsResponse = googleMapsDirectionsResponse;
            _googleMapsApiDeserialiser = googleMapsApiDeserialiser;
            _specifyConditionsOfNoTaxiRoutesFound = noTaxiRoutesSpecification;
        }

        public DistanceCalculator()
        {
            _googleMapsDirectionsResponse = new GoogleMapsDirectionsResponse();
            _googleMapsApiDeserialiser = new GoogleMapsApiDeserialiser();
        }

        public string Calculate(string from, string to)
        {
            string response = _googleMapsDirectionsResponse.Generate(from, to);

            DirectionsResponse googleMapsDirections =
                _googleMapsApiDeserialiser.DeserializeResponse(response);

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