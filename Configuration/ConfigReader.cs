using System.Configuration;

namespace Configuration
{
    public interface ICanReadConfigurations
    {
        string TaxiApiUrl();
        string TaxiApiKey();
        string GooglePlacesApiKey();
    }

    public class ConfigReader : ICanReadConfigurations
    {
        public string TaxiApiUrl()
        {
            return ConfigurationSetting("YourTaxiApiUri");
        }

        public string TaxiApiKey()
        {
            return ConfigurationSetting("YourTaxiApiKey");
        }

        public string GooglePlacesApiKey()
        {
            return ConfigurationSetting("GooglePlacesApiKey");
        }

        private static string ConfigurationSetting(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }
    }
}