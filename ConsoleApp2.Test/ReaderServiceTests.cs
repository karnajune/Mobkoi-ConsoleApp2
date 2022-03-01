using ConsoleApp2.Implementations;
using ConsoleApp2.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ConsoleApp2.Test
{
    public class ReaderServiceTests
    {
        [Fact]
        public void Test_SuccessfulReport_ReturnSuccess()
        {
            var entityName = "account";
            var columnNames = new string[] { "name", "parentaccountid", "primarycontactid", "telephone1", "emailaddress1", "revenue", "revenue_base", "creditlimit", "creditlimit_base" };
            var connectorService = new Mock<ICrmConnectorService>();
            var loggerService = new Mock<ILogger<ReaderService>>();
            var helperService = new Mock<IHelperService>();
            var csvParserService = new Mock<ICsvParserService>();
            var fxService = new Mock<IFxService>();

            var readerService = new ReaderService(connectorService.Object, loggerService.Object, helperService.Object, csvParserService.Object, fxService.Object);
            var result = readerService.GetCsvReport(entityName,columnNames);

            Assert.True(result.Item1);// this test is failing as I am finding it difficult to install CrmSdk to test projects or use different mocking libraries for connection.
        }
    }
}
