using ConsoleApp2.Implementations;
using ConsoleApp2.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ConsoleApp2.Test
{
    public class CsvParserTests
    {
        [Fact]
        public void Test_SuccessfulParsing_ReturnTrueAndFileName()
        {
            var mockLoggerService = new Mock<ILogger<CsvParserService>>();

            var csvParser = new CsvParserService(mockLoggerService.Object);

            var result = csvParser.ExportToCsv(new List<Account>());

            Assert.True(result.Item1);
            Assert.True(!string.IsNullOrEmpty(result.Item2));
        } 
    }
}
