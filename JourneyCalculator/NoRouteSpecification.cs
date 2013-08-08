namespace JourneyCalculator
{
    public interface ISpecifyConditionsOfNoTaxiRoutesFound
    {
        bool IsSatisfiedBy(DirectionsResponse googleMapsDirections);
    }
}