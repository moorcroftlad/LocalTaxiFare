using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TaxiFirmDetails
{
    public interface IRetrieveTaxiFirms
    {
        List<TaxiFirm> Create(string latlong);
    }

    public partial class TaxiFirmRetriever : IRetrieveTaxiFirms
    {
        private readonly IConstructGoogleTextSearchRequests _googleTextSearchRequestConstructor;
        private readonly IConstructGooglePlaceRequests _googlePlaceRequestConstructor;

        public TaxiFirmRetriever(IConstructGoogleTextSearchRequests googleTextSearchRequestConstructor, IConstructGooglePlaceRequests googlePlaceRequestConstructor)
        {
            _googleTextSearchRequestConstructor = googleTextSearchRequestConstructor;
            _googlePlaceRequestConstructor = googlePlaceRequestConstructor;
        }

        public List<TaxiFirm> Create(string latlong)
        {
            var localTaxis = _googleTextSearchRequestConstructor.GetTextSearchRequests(latlong);
            var places = JsonConvert.DeserializeObject<GooglePlaces>(localTaxis);
            return places.Results.Select(TaxiFirm).ToList();
        }

        private TaxiFirm TaxiFirm(GooglePlacesResults firstGooglePlacesResults)
        {
            var companyName = firstGooglePlacesResults.Name;
            var placeReference = firstGooglePlacesResults.Reference;
            var response = _googlePlaceRequestConstructor.GetPlaceRequest(placeReference);
            var place = JsonConvert.DeserializeObject<GooglePlace>(response);
            var googlePlaceResult = place.Result;
            var formattedPhoneNumber = googlePlaceResult.Formatted_Phone_Number;
            var taxiFirm = new TaxiFirm {Name = companyName, Number = formattedPhoneNumber};
            return taxiFirm;
        }
    }

    public class FakeTaxiFirmRetriever : IRetrieveTaxiFirms
    {
        public List<TaxiFirm> Create(string latlong)
        {
            return new List<TaxiFirm>
                {
                    new TaxiFirm
                        {
                            Name = "Manchester Cabs",
                            Number = "07712345678"
                        },
                    new TaxiFirm
                        {
                            Name = "Chester Cabs",
                            Number = "01615646533"
                        },
                    new TaxiFirm
                        {
                            Name = "Liverpool Cabs",
                            Number = "01615451203"
                        }
                };
        }
    }
}