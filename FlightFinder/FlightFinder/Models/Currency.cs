using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Currency
    {
        public string Code { get; set; }
        public decimal DecimalDigits { get; set; }
        public string DecimalSeparator { get; set; }
        public int RoundingCoefficient { get; set; }
        public bool SpaceBetweenAmountAndSymbol { get; set; }
        public string Symbol { get; set; }
        public bool SymbolOnLeft { get; set; }
        public string ThousandsSeparator { get; set; }

    }
}
