using ConsoleApp2.Interfaces;
using ConsoleApp2.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp2.Implementations
{
    public class FxService : IFxService
    {
        private readonly string currentMonthAndYear;
        private readonly string fxUrl;
        private readonly ILogger<FxService> _logger;       

        public FxService(ILogger<FxService> logger)
        {
            _logger = logger;
            currentMonthAndYear = DateTime.Now.ToString("MMyy"); // this date could be taken from account created date if they are different for accounts;
            fxUrl= ConfigurationManager.AppSettings.Get("FxRates") + currentMonthAndYear + ".XML";
        }
        public ExchangeRate GetFxRate()
        {
            var exchangeRate=new ExchangeRate();
            var request = WebRequest.Create(fxUrl);
            try
            {
                using (StreamReader streamIn = new StreamReader((request.GetResponse()).GetResponseStream()))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ExchangeList));
                    var exchangeRates = (ExchangeList)serializer.Deserialize(streamIn);// loading all rates when needed only one, need to look for a different way to call the HMRC
                    exchangeRate=exchangeRates.ExchangeRates.Where(x => x.CountryName.Equals("USA")).FirstOrDefault();
                    streamIn.Close();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Could not load or parse Fx Rates{ex.Message}");
            }
            finally
            {
                request.Abort();
            }

            return exchangeRate;
        }
    }
}
