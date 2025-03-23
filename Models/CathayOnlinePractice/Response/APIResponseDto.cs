using Common.Extensions;
using System.Text.Json.Serialization;
using static Common.Enums.WebApiEnum;

namespace Models.CathayOnlinePractice.Response
{
    public class ApiResponseBaseDto
    {
        /// <summary>
        /// 回傳代碼
        /// </summary>
        [JsonIgnore]
        public APIResponseEnum ResponseEnum { get; set; }

        /// <summary>
        /// 回傳代碼
        /// </summary>
        public string ReturnCode
        {
            get
            {
                return ResponseEnum.GetEnumName();
            }
        }

        /// <summary>
        /// 回傳說明
        /// </summary>
        public string ReturnMsg
        {
            get
            {
                return ResponseEnum.GetEnumDescription();
            }
        }

        /// <summary>
        /// 回傳日期時間
        /// 格式：yyyyMMddHHmmss
        /// </summary>
        public string ReturnTime
        {
            get
            {
                return $"{DateTime.Now:yyyyMMddHHmmss}";
            }
        }
    }
    public class APIResponseDto : ApiResponseBaseDto
    {
    }
    public class APIResponseDto<T> : ApiResponseBaseDto
    {

        /// <summary>
        /// 回傳內容
        /// </summary>
        public T OutData { get; set; }
    }
}
