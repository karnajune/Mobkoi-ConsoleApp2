using ConsoleApp2.Interfaces;
using ConsoleApp2.Models;
using Microsoft.Extensions.Logging;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Implementations
{
    public class CsvParserService : ICsvParserService
    {
        
        private readonly ILogger<CsvParserService> _logger;
        private readonly string filePath;
        private readonly string delimeter = ",";
        
        public CsvParserService(ILogger<CsvParserService> logger)
        {
            _logger = logger;
            filePath = Path.Combine(Environment.CurrentDirectory, DateTime.Now.ToString("ddMMyyyy") + "-AccountReports.csv");
        }

        public Tuple<bool,string> ExportToCsv(List<Account> accountsList)
        {
            try
            {
                var lines = new List<string>();
                var props = TypeDescriptor.GetProperties(typeof(Account)).OfType<PropertyDescriptor>();
                var header = string.Join(delimeter, props.ToList().Select(x => x.Name));
                lines.Add(header);
                var valueLines = accountsList.Select(row => string.Join(delimeter, header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
                lines.AddRange(valueLines);

                File.WriteAllLines(filePath, lines.ToArray());
                return Tuple.Create(true,filePath);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to download Csv {ex.Message}");
                return Tuple.Create(false, string.Empty);
            }
        }
    }
}
