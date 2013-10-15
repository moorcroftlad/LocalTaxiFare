using System;
using System.Text;
using Configuration;

namespace TaxiApi.Request
{
    public interface ICreateTaxiFareRequests
    {
        string Create(DateTime date, string distance, string fromLatLong);
    }

    public class TaxiFareRequest : ICreateTaxiFareRequests
    {
        private readonly IReadConfiguration _readConfiguration;

        public TaxiFareRequest(IReadConfiguration readConfiguration)
        {
            _readConfiguration = readConfiguration;
        }

        public TaxiFareRequest()
        {
            _readConfiguration = new ConfigReader();
        }

        public string Create(DateTime date, string distance, string fromLatLong)
        {
            var request = new StringBuilder();

            request.Append(string.Format("?key={0}", _readConfiguration.TaxiApiKey()));
            request.Append("&return=json");
            request.Append("&type=fare");
            request.Append("&passengers=1");
            request.Append("&mobile=0");
            request.Append(string.Format("&year={0}", date.Year));
            request.Append(string.Format("&month={0}", date.Month));
            request.Append(string.Format("&day={0}", date.Day));
            request.Append(string.Format("&hour={0}", date.Hour));
            request.Append(string.Format("&minute={0}", date.Minute));
            request.Append(string.Format("&distance={0}", distance));
            request.Append(string.Format("&from={0}", fromLatLong));

            return request.ToString();
        }
    }
}