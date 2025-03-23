using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Currency")]
    public class Currency : BaseEntity
    {
        /// <summary>
        /// 流水編號
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 幣別名稱
        /// </summary>
        [Required]
        [Display(Name = "幣別代碼"), MaxLength(3)]
        public string Code { get; set; }

        /// <summary>
        /// 幣別名稱
        /// </summary>
        [Required]
        [Display(Name = "幣別名稱"), MaxLength(20)]
        public string Name { get; set; }
    }
}
