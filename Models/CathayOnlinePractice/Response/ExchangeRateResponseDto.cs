using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.CathayOnlinePractice.Response
{
    public class ExchangeRateResponseDto
    {
        /// <summary>
        /// 更新時間
        /// </summary>
        [Display(Name = "更新時間")]
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 幣別匯率清單
        /// </summary>
        [Display(Name = "幣別匯率清單")]
        public List<ExchangeRateCurrencyInfo> Currencies { get; set; }
    }

    public class ExchangeRateCurrencyInfo
    {
        /// <summary>
        /// 幣別代碼（例如：USD、EUR、GBP）
        /// </summary>
        [JsonPropertyName("code")]
        [Display(Name = "幣別代碼")]
        public string Code { get; set; }

        /// <summary>
        /// 幣別中文名稱（例如：美元、歐元、英鎊）
        /// </summary>
        [Display(Name = "幣別中文名稱")]
        public string Name { get; set; }

        /// <summary>
        /// 匯率（例如：23342.0112）
        /// </summary>
        [Display(Name = "匯率")]
        public float Rate { get; set; }
    }
}
