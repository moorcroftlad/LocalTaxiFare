using Geography;

namespace LocalTaxiFare
{
    public interface ICreateLocations
    {
        Location GetLocation(string latlong);
    }
}