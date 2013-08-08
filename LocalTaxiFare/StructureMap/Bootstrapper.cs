using System.Web.Mvc;
using Configuration;
using JourneyCalculator;
using Results;
using StructureMap;
using TaxiApi.Request;
using TaxiApi.Response;
using TaxiFirmDetails;
using WebResponse;

namespace LocalTaxiFare.StructureMap
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
            ObjectFactory.Initialize(expression =>
                {
                    expression.For<ICanReadConfigurations>().Use<ConfigReader>();
                    expression.For<IDownloadResponses>().Use<WebClientWrapper>();
                    expression.For<IPerformApiRequest>().Use<WebClientApiRequest>();
                    expression.For<ICreateRequests>().Use<FareRequestFactory>();
                    expression.For<ICreateResponses>().Use<FareResponseFactory>();
                    expression.For<IGetTheResponseFromGoogleMapsDirectionsApi>().Use<GoogleMapsDirectionsResponse>();
                    expression.For<IDeserialiseGoogleMapsDirectionsResponses>().Use<GoogleMapsApiDeserialiser>();
                    expression.For<ICanGetTheDistanceOfATaxiJourneyBetweenPoints>().Use<DistanceCalculator>();
                    expression.For<IConstructGoogleTextSearchRequests>().Use<GoogleTextSearchRequestConstructor>();
                    expression.For<IConstructGooglePlaceRequests>().Use<GooglePlaceRequestConstructor>();
                    expression.For<ICalculateTheTaxiFare>().Use<TaxiFareCalculator>();
                    expression.For<ITaxiFirmFactory>().Use<TaxiFirmFactory>();
                });
        }
    }
}