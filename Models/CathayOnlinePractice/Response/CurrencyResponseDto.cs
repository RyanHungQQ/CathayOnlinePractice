using Models.Mappings;

namespace Models.CathayOnlinePractice.Response
{
    /// <summary>
    /// 幣別傳回資訊
    /// </summary>
    public class CurrencyResponseDto
    {
        /// <summary>
        /// 流水編號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 幣別名稱
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 幣別名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 新增時間
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        public DateTime? ModifyDate { get; set; }
    }
}
