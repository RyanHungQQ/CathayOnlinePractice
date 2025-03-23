using Common.Enums;
using FluentAssertions;
using Lib.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CathayOnlinePractice.Response;
using Models.CathayOnlinePractice.Resqust;
using Moq;
using WebApi.Controllers;

namespace Test
{
    public class CurrencyControllerTest
    {
        private readonly Mock<ICurrencyService> _currencyServiceMock;
        private readonly CurrencyController _controller;

        public CurrencyControllerTest()
        {
            // 創建 Moq 模擬的 ICurrencyService 實例
            _currencyServiceMock = new Mock<ICurrencyService>();
            _controller = new CurrencyController(_currencyServiceMock.Object);
        }

        // 測試 GetCurrencies 方法，確保返回所有的幣別資料
        [Fact]
        public async Task GetCurrencies_ShouldReturnAllCurrencies()
        {
            // Arrange: 模擬 GetAllCurrenciesAsync 返回的資料
            var currencies = new List<CurrencyResponseDto>
            {
                new CurrencyResponseDto { Id = 1, Code = "USD", Name = "US Dollar", CreateDate = DateTime.Now, ModifyDate = DateTime.Now },
                new CurrencyResponseDto { Id = 2, Code = "EUR", Name = "Euro", CreateDate = DateTime.Now, ModifyDate = DateTime.Now }
            };

            _currencyServiceMock.Setup(service => service.GetAllCurrenciesAsync())
                .ReturnsAsync(currencies);

            // Act: 呼叫控制器方法
            var result = await _controller.GetCurrencies();

            // Assert: 驗證返回的結果
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrencies = okResult.Value as IEnumerable<CurrencyResponseDto>;
            returnedCurrencies.Should().BeEquivalentTo(currencies);  // 期望返回的資料和模擬資料一致
        }

        // 測試 GetCurrencyById 方法，確保能根據 ID 正確返回幣別資料
        [Fact]
        public async Task GetCurrencyById_ShouldReturnCurrency_WhenExists()
        {
            //模擬資料庫中的幣別資料
            var currency = new CurrencyResponseDto { Id = 1, Code = "USD", Name = "US Dollar", CreateDate = DateTime.Now, ModifyDate = DateTime.Now };
            //回傳共用Dto
            var response = new APIResponseDto<CurrencyResponseDto>() { ResponseEnum = WebApiEnum.APIResponseEnum.Success, OutData = currency };
            _currencyServiceMock.Setup(service => service.GetCurrencyByIdAsync(1))
                .ReturnsAsync(response);

            // Act: 呼叫控制器方法，請求 ID 為 1 的幣別資料
            var result = await _controller.GetCurrency(1);

            // Assert: 驗證返回的結果
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrency = okResult.Value as APIResponseDto<CurrencyResponseDto>;
            returnedCurrency.Should().BeEquivalentTo(response);  // 期望返回的資料與模擬資料一致
        }

        // 測試 GetCurrencyById 方法，當資料不存在時，能夠回傳對應錯誤代碼
        [Fact]
        public async Task GetCurrencyById_ShouldReturnNotFound_WhenNotExists()
        {
            //回傳共用Dto
            var response = new APIResponseDto<CurrencyResponseDto>() { ResponseEnum = WebApiEnum.APIResponseEnum.NotFound };
            // Arrange: 模擬資料庫中找不到 ID 為 1 的幣別
            _currencyServiceMock.Setup(service => service.GetCurrencyByIdAsync(1))
                .ReturnsAsync(response);

            // Act: 呼叫控制器方法，請求 ID 為 1 的幣別資料
            var result = await _controller.GetCurrency(1);

            // Assert: 驗證返回的結果
            var notFoundResult = result as OkObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrency = notFoundResult.Value as APIResponseDto<CurrencyResponseDto>;
            returnedCurrency.Should().BeEquivalentTo(response);  // 期望返回的資料與模擬資料一致
        }

        // 測試 PostCurrency 方法，確保能夠成功創建幣別資料
        [Fact]
        public async Task PostCurrency_ShouldReturnCreatedCurrency()
        {
            // Arrange: 模擬前端發送的幣別資料
            var currencyDto = new CurrnecyResqustDto { Code = "USD", Name = "US Dollar" };
            var createdCurrency = new CurrencyResponseDto { Code = "USD", Name = "US Dollar" };
            //回傳共用Dto
            var response = new APIResponseDto<CurrencyResponseDto>() { ResponseEnum = WebApiEnum.APIResponseEnum.Success, OutData = createdCurrency };

            _currencyServiceMock.Setup(service => service.CreateCurrencyAsync(currencyDto))
                .ReturnsAsync(response);

            // Act: 呼叫控制器方法，創建幣別資料
            var result = await _controller.PostCurrency(currencyDto);

            // Assert: 驗證返回的結果
            var createdResult = result as OkObjectResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrency = createdResult.Value as APIResponseDto<CurrencyResponseDto>;
            returnedCurrency.Should().BeEquivalentTo(response);  // 期望返回創建的幣別資料
        }

        // 測試 PutCurrency 方法，確保能夠成功更新幣別資料
        [Fact]
        public async Task PutCurrency_ShouldReturnNoContent_WhenUpdated()
        {
            // Arrange: 模擬前端發送的幣別更新資料
            var currencyDTO = new CurrnecyResqustDto { Code = "EUR", Name = "Euro" };
            var currency = new CurrencyResponseDto { Code = "EUR", Name = "Euro" };
            var response = new APIResponseDto<CurrencyResponseDto>() { ResponseEnum = WebApiEnum.APIResponseEnum.Success, OutData = currency };

            _currencyServiceMock.Setup(service => service.UpdateCurrencyAsync(1, currencyDTO))
                .ReturnsAsync(response);

            // Act: 呼叫控制器方法，更新幣別資料
            var result = await _controller.PutCurrency(1, currencyDTO);

            // Assert: 驗證返回的結果
            var updatedResult = result as OkObjectResult;
            updatedResult.Should().NotBeNull();
            updatedResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrency = updatedResult.Value as APIResponseDto<CurrencyResponseDto>;
            returnedCurrency.Should().BeEquivalentTo(response);  // 期望返回創建的幣別資料
        }

        // 測試 DeleteCurrency 方法，確保能夠成功刪除幣別資料
        [Fact]
        public async Task DeleteCurrency_ShouldReturnNoContent_WhenDeleted()
        {
            //回傳共用Dto
            var response = new APIResponseDto() { ResponseEnum = WebApiEnum.APIResponseEnum.Success };
            // Arrange: 模擬幣別刪除操作
            _currencyServiceMock.Setup(service => service.DeleteCurrencyAsync(1))
                .ReturnsAsync(response);

            // Act: 呼叫控制器方法，刪除幣別資料
            var result = await _controller.DeleteCurrency(1);

            // Assert: 驗證返回的結果
            var notFoundResult = result as OkObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrency = notFoundResult.Value as APIResponseDto;
            returnedCurrency.Should().BeEquivalentTo(response);  // 期望返回的資料與模擬資料一致
        }

        // 測試 DeleteCurrency 方法，當資料不存在時，應返回 NotFound (404)
        [Fact]
        public async Task DeleteCurrency_ShouldReturnNotFound_WhenNotExist()
        {
            //回傳共用Dto
            var response = new APIResponseDto() { ResponseEnum = WebApiEnum.APIResponseEnum.NotFound };
            // Arrange: 模擬刪除操作，但資料不存在
            _currencyServiceMock.Setup(service => service.DeleteCurrencyAsync(1))
                .ReturnsAsync(response);

            // Act: 呼叫控制器方法，刪除幣別資料
            var result = await _controller.DeleteCurrency(1);

            // Assert: 驗證返回的結果
            var notFoundResult = result as OkObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(200);  // 期望返回 200 OK

            var returnedCurrency = notFoundResult.Value as APIResponseDto;
            returnedCurrency.Should().BeEquivalentTo(response);  // 期望返回的資料與模擬資料一致
        }
    }
}
