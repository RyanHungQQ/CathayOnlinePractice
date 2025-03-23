using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models.Dto
{
    public class BitcoinPriceDto
    {
        public BitcoinPriceTime Time { get; set; }
        public string Disclaimer { get; set; }
        public string ChartName { get; set; }
        public BitcoinPriceBpi Bpi { get; set; }
    }
    public class BitcoinPriceTime
    {
        /// <summary>
        /// 原始更新時間字串（例如：Aug 3, 2022 20:25:00 UTC）
        /// </summary>
        [Display(Name = "原始更新時間")]
        public string Updated { get; set; }

        /// <summary>
        /// 更新時間（ISO 8601 格式，自動對應 DateTime）
        /// </summary>
        [Display(Name = "更新時間")]
        public DateTime UpdatedISO { get; set; }

        /// <summary>
        /// 英國時間格式字串（例如：Aug 3, 2022 at 21:25 BST）
        /// </summary>
        [Display(Name = "英國時間")]
        public string Updateduk { get; set; }
    }

    public class BitcoinPriceBpi
    {
        public BitcoinPriceCurrency USD { get; set; }
        public BitcoinPriceCurrency GBP { get; set; }
        public BitcoinPriceCurrency EUR { get; set; }
    }

    public class BitcoinPriceCurrency
    {
        /// <summary>
        /// 貨幣代碼，例如 USD、EUR
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 貨幣符號，例如 $、€
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 價格字串格式（含千分位）
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// 貨幣說明，例如 US Dollar
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 價格的數值格式
        /// </summary>
        [JsonProperty("rate_float")]
        public float RateFloat { get; set; }
    }
}
