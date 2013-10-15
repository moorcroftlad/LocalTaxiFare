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
            var controllerFactory = new ControllerFactory();
            var configReader = new ConfigReader();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            ObjectFactory.Initialize(expression =>
                {
                    expression.For<IReadConfiguration>().Use<ConfigReader>();
                    expression.For<IDownloadResponses>().Use<WebClientWrapper>();
                    expression.For<IPerformApiRequest>().Use<WebClientApiRequest>();
                    expression.For<ICreateTaxiFareRequests>().Use<TaxiFareRequest>();
                    expression.For<IGetTheResponseFromGoogleMapsDirectionsApi>().Use<GoogleMapsDirectionsResponse>();
                    expression.For<IDeserialiseGoogleMapsDirectionsResponses>().Use<GoogleMapsApiDeserialiser>();
                    expression.For<IConstructGoogleTextSearchRequests>().Use<GoogleTextSearchRequestConstructor>();
                    expression.For<IConstructGooglePlaceRequests>().Use<GooglePlaceRequestConstructor>();
                    
                    if (configReader.CallLiveApi())
                    {
                        expression.For<ICalculateTheTaxiFare>().Use<TaxiFareCalculator>();
                        expression.For<IRetrieveTaxiFirms>().Use<TaxiFirmRetriever>();
                        expression.For<ICalculateDistanceBetweenLatLong>().Use<DistanceCalculator>();   
                    }
                    else
                    {
                        expression.For<ICalculateTheTaxiFare>().Use<FakeTaxiFareCalculator>();
                        expression.For<IRetrieveTaxiFirms>().Use<FakeTaxiFirmRetriever>();
                        expression.For<ICalculateDistanceBetweenLatLong>().Use<FakeDistanceCalculator>();   
                    }
                });
        }
    }
}