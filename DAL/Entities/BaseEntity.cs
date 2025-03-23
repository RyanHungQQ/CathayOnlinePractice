using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class BaseEntity
    {

        /// <summary>
        /// 新增時間
        /// </summary>
        [Display(Name = "新增時間")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        [Display(Name = "修改時間")]
        public DateTime? ModifyDate { get; set; }
    }
}
