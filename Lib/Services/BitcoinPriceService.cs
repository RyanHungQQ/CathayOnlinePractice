using Lib.Interfaces.Services;
using Lib.Mocks;
using Models.CathayOnlinePractice.Response;
using Models.Dto;
using Newtonsoft.Json;
using NLog;
using ILogger = NLog.ILogger;

namespace Lib.Services
{
    public class BitcoinPriceService : IBitcoinPriceService
    {
        private readonly HttpClient _httpClient;
        private readonly ICurrencyService _currencyService;
        private readonly ILogger _logger;

        public BitcoinPriceService(HttpClient httpClient, ICurrencyService currencyService)
        {
            _httpClient = httpClient;
            _logger = LogManager.GetCurrentClassLogger();
            _currencyService = currencyService;
        }

        /// <summary>
        /// 呼叫 coindesk 的 API，並進行資料轉換，組成新 API
        /// </summary>
        /// <returns></returns>
        public async Task<ExchangeRateResponseDto> GetExchangeRateData()
        {
            var bitcoinPriceDto = await GetBitcoinPriceData();
            var result = new ExchangeRateResponseDto()
            {
                UpdatedTime = bitcoinPriceDto.Time.UpdatedISO,
                Currencies = await TransCurrency(bitcoinPriceDto.Bpi)
            };
            return result;
        }
        private Task<List<ExchangeRateCurrencyInfo>> TransCurrency(BitcoinPriceBpi bpi)
        {
            var currencies = new List<ExchangeRateCurrencyInfo>();

            // 透過反射取得 Bpi 所有屬性
            var currencyProperties = bpi.GetType().GetProperties();

            foreach (var prop in currencyProperties)
            {
                var currency = prop.GetValue(bpi) as BitcoinPriceCurrency;
                if (currency == null)
                    continue;

                currencies.Add(new ExchangeRateCurrencyInfo
                {
                    Code = currency.Code,
                    Rate = currency.RateFloat
                });
            }
            return MappingCurrency(currencies);
        }
        private async Task<List<ExchangeRateCurrencyInfo>> MappingCurrency(List<ExchangeRateCurrencyInfo> currencyInfos)
        {
            if (currencyInfos == null || currencyInfos.Count == 0)
                return currencyInfos;

            var currencyResponses = await _currencyService.GetAllCurrenciesAsync();
            currencyInfos.ForEach(o => o.Name = currencyResponses.FirstOrDefault(x => x.Code == o.Code)?.Name ?? o.Code);
            return currencyInfos;
        }

        #region 呼叫 coindesk API
        /// <summary>
        /// 將coindesk API取得的Json轉換成對應Dto資料
        /// </summary>
        /// <returns></returns>
        public async Task<BitcoinPriceDto> GetBitcoinPriceData()
        {
            var jsonString = await GetCurrentPriceJson();
            if (string.IsNullOrEmpty(jsonString))
                return new BitcoinPriceDto();

            return JsonConvert.DeserializeObject<BitcoinPriceDto>(jsonString);
        }
        /// <summary>
        /// 呼叫 coindesk API，若API Call失敗則從取得模擬資料
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetCurrentPriceJson()
        {
            string? jsonString;
            try
            {
                string apiUrl = "https://api.coindesk.com/v1/bpi/currentprice.json";
                _logger.Info($"Calling external API: {apiUrl}");
                jsonString = await _httpClient.GetStringAsync(apiUrl);
                _logger.Info($"External API Response: {jsonString}");
            }
            catch (Exception ex)
            {
                // 紀錄錯誤資訊
                _logger.Error(ex, "Error fetching data from CoinDesk API");
                jsonString = JsonMock.GetBitcoinPriceJson();
            }
            return jsonString;
        }
        #endregion
    }
}
