using Lib.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CathayOnlinePractice.Response;
using Models.CathayOnlinePractice.Resqust;

namespace WebApi.Controllers
{
    /// <summary>
    /// 幣別 DB 維護功能
    /// </summary>
    public class CurrencyController : BaseAPIController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        // GET: api/Currencies
        /// <summary>
        /// 查詢所有幣別資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(APIResponseDto<IEnumerable<CurrencyResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCurrencies()
        {
            var currencies = await _currencyService.GetAllCurrenciesAsync();
            return Ok(currencies);
        }

        // GET: api/Currencies/5
        /// <summary>
        /// 查詢幣別資料
        /// </summary>
        /// <param name="id">流水號</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(APIResponseDto<CurrencyResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCurrency(int id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        // POST: api/Currencies
        /// <summary>
        /// 新增幣別資料
        /// </summary>
        /// <param name="currency">幣別資料</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(APIResponseDto<CurrencyResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> PostCurrency(CurrnecyResqustDto currency)
        {
            var createdCurrency = await _currencyService.CreateCurrencyAsync(currency);
            return Ok(createdCurrency);
        }

        // PUT: api/Currencies/5
        /// <summary>
        /// 修改幣別資料
        /// </summary>
        /// <param name="id">流水號</param>
        /// <param name="currency">幣別資料</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(APIResponseDto<CurrencyResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> PutCurrency(int id, CurrnecyResqustDto currency)
        {
            var updatedCurrency = await _currencyService.UpdateCurrencyAsync(id, currency);
            if (updatedCurrency == null)
            {
                return NotFound();
            }
            return Ok(updatedCurrency);
        }

        // DELETE: api/Currencies/5
        /// <summary>
        /// 刪除幣別資料
        /// </summary>
        /// <param name="id">流水號</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(APIResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteCurrency(int id)
        {
            var result = await _currencyService.DeleteCurrencyAsync(id);
            return Ok(result);
        }
    }
}
