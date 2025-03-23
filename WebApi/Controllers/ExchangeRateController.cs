using Common.Attributes;
using Lib.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Models.CathayOnlinePractice.Response;

namespace WebApi.Controllers
{
    [IgnoreResponse]
    public class ExchangeRateController : BaseAPIController
    {
        private readonly IBitcoinPriceService _bitcoinPriceService;

        public ExchangeRateController(IBitcoinPriceService bitcoinPriceService)
        {
            _bitcoinPriceService = bitcoinPriceService;
        }
        /// <summary>
        /// 呼叫 coindesk 的 API，並進行資料轉換
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ExchangeRateResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrency()
        {
            var response = await _bitcoinPriceService.GetExchangeRateData();
            return Ok(response);
        }
    }
}
