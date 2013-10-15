using System.Configuration;

namespace Configuration
{
    public interface IReadConfiguration
    {
        string TaxiApiUrl();
        string TaxiApiKey();
        string GooglePlacesApiKey();
        bool CallLiveApi();
    }

    public class ConfigReader : IReadConfiguration
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

        public bool CallLiveApi()
        {
            bool result;
            bool.TryParse(ConfigurationSetting("CallLiveApi"), out result);
            return result;
        }

        private static string ConfigurationSetting(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }
    }
}