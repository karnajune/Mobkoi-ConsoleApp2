using ConsoleApp2.Implementations;
using ConsoleApp2.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ConsoleApp2.Test
{
    public class FxServiceTests
    {
        [Fact]
        public void Test_SuccessfulFxRate_ReturnUSA()
        {
            var mockLoggerService = new Mock<ILogger<FxService>>();

            var fxService = new FxService(mockLoggerService.Object);
            var result = fxService.GetFxRate();

            Assert.Equal("USA",result.CountryCode);

        }
        
        [Fact]
        public void Test_SuccessfulFxRate_ReturnNonNegative()
        {
            var mockLoggerService = new Mock<ILogger<FxService>>();

            var fxService = new FxService(mockLoggerService.Object);
            var result = fxService.GetFxRate();

            Assert.NotEqual(-1,result.RateNew);

        }
    }
}
