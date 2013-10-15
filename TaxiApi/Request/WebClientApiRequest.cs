using Configuration;
using WebResponse;

namespace TaxiApi.Request
{
    public class WebClientApiRequest : IPerformApiRequest
    {
        private readonly IReadConfiguration _configReader;
        private readonly IDownloadResponses _webResponseReader;

        public WebClientApiRequest(IReadConfiguration configReader, IDownloadResponses webResponseReader)
        {
            _configReader = configReader;
            _webResponseReader = webResponseReader;
        }

        public WebClientApiRequest()
        {
            _configReader = new ConfigReader();
            _webResponseReader = new WebClientWrapper();
        }

        public string Perform(string request)
        {
            var formattedRequest = string.Concat(_configReader.TaxiApiUrl(), request);
            return _webResponseReader.Get(formattedRequest);
        }
    }
}