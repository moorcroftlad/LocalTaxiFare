using System.Web.Mvc;
using Results;
using TaxiFirmDetails;
using JourneyCalculator;
using LocalTaxiFare.Models;

namespace LocalTaxiFare.Controllers
{
    public class TaxiController : Controller
    {
        private readonly ICalculateTheTaxiFare _taxiFareCalculator;
        private readonly IRetrieveTaxiFirms _retrieveTaxiFirms;
        private readonly ICalculateDistanceBetweenLatLong _distanceCalculator;

        public TaxiController(ICalculateTheTaxiFare taxiFareCalculator, IRetrieveTaxiFirms retrieveTaxiFirms, ICalculateDistanceBetweenLatLong distanceCalculator)
        {
            _taxiFareCalculator = taxiFareCalculator;
            _retrieveTaxiFirms = retrieveTaxiFirms;
            _distanceCalculator = distanceCalculator;
        }

        public ViewResult Index(string fromLat, string fromLong, string toLat, string toLong)
        {
            var fromLatLong = fromLat + "," + fromLong;
            var toLatLong = toLat + "," + toLong;
            var distance = _distanceCalculator.Calculate(fromLatLong, toLatLong);
            var taxiPrice = _taxiFareCalculator.GetTaxiPrice(distance, fromLatLong);
            var taxis = _retrieveTaxiFirms.Create(fromLatLong);
            var viewModel = new TaxisViewModel
            {
                TaxiFirms = taxis,
                AveragePrice = (decimal)taxiPrice
            };
            return View("Index", viewModel);
        }
    }
}