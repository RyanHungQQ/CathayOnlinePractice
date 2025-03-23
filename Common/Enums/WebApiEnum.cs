using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public class WebApiEnum
    {
        public enum APIResponseEnum
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Display(Name = "0000", Description = "成功")]
            Success = 0000,

            /// <summary>
            /// 失敗
            /// </summary>
            [Display(Name = "9000", Description = "失敗")]
            Fail = 9000,

            /// <summary>
            /// 查無資料
            /// </summary>
            [Display(Name = "9001", Description = "查無資料")]
            NotFound = 9001,

            /// <summary>
            /// 幣別資料已存在，請勿重複新增
            /// </summary>
            [Display(Name = "9002", Description = "幣別資料已存在，請勿重複新增")]
            DataExist = 9002,

            /// <summary>
            /// Exception
            /// </summary>
            [Display(Name = "9999", Description = "系統處理異常")]
            Exception = 9999
        }
    }
}
