using FluentAssertions;
using Lib.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CathayOnlinePractice.Response;
using Moq;
using WebApi.Controllers;

namespace Test
{
    public class ExchangeRateControllerTest
    {
        private readonly Mock<IBitcoinPriceService> _bitcoinPriceServiceMock;
        private readonly ExchangeRateController _controller;

        public ExchangeRateControllerTest()
        {
            _bitcoinPriceServiceMock = new Mock<IBitcoinPriceService>();
            _controller = new ExchangeRateController(_bitcoinPriceServiceMock.Object);
        }
        [Fact]
        public async Task GetCurrency()
        {
            // Arrange: 模擬 GetCurrency 返回的資料
            var exchangeRate = new ExchangeRateResponseDto()
            {
                UpdatedTime = DateTime.Now,
                Currencies = new List<ExchangeRateCurrencyInfo>()
                {
                    new ExchangeRateCurrencyInfo { Code = "USD", Name = "US Dollar", Rate = 0 },
                    new ExchangeRateCurrencyInfo { Code = "EUR", Name = "Euro", Rate = 0} 
                }
            };

            _bitcoinPriceServiceMock.Setup(service => service.GetExchangeRateData())
                .ReturnsAsync(exchangeRate);

            // Act: 呼叫控制器方法
            var result = await _controller.GetCurrency();

            // Assert: 驗證返回的結果
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrencies = okResult.Value as ExchangeRateResponseDto;
            returnedCurrencies.Should().BeEquivalentTo(exchangeRate);  // 期望返回的資料和模擬資料一致
        }
    }
}
