using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp2.Models
{    
    public class ExchangeRate
    {
        [XmlElement("countryName")] public string CountryName { get; set; }
        [XmlElement("countryCode")] public string CountryCode { get; set; }
        [XmlElement("currencyName")] public string CurrencyName { get; set; }
        [XmlElement("currencyCode")] public string CurrencyCode { get; set; }
        [XmlElement("rateNew")] public decimal RateNew { get; set; }
    }

    [XmlRoot("exchangeRateMonthList")]
    public class ExchangeList
    {
        [XmlElement("exchangeRate", typeof(ExchangeRate))]
        public List<ExchangeRate> ExchangeRates { get; set; }
    }
}
