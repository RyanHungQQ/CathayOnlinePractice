using System.ComponentModel.DataAnnotations;

namespace Models.CathayOnlinePractice.Resqust
{
    public class CurrnecyResqustDto
    {
        /// <summary>
        /// 幣別代碼
        /// </summary>
        [Display(Name = "幣別代碼"), MaxLength(3)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 幣別名稱
        /// </summary>
        [Display(Name = "幣別名稱"), MaxLength(20)]
        [Required]
        public string Name { get; set; }
    }
}
