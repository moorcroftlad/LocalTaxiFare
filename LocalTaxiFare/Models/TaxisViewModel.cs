using System.Collections.Generic;
using TaxiFirmDetails;

namespace LocalTaxiFare.Models
{
    public class TaxisViewModel
    {
        public IEnumerable<TaxiFirm> TaxiFirms { get; set; }
        public decimal AveragePrice { get; set; }
    }
}