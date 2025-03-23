using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.CathayOnlinePractice.Response;
using NLog;
using static Common.Enums.WebApiEnum;

namespace WebApi.Attributes
{
    /// <summary>
    /// 異常處理
    /// </summary>
    public class ExceptionAttribute: ExceptionFilterAttribute
    {
        #region private
        private readonly Logger _loggerProcess;
        #endregion

        public ExceptionAttribute()
        {
            _loggerProcess = LogManager.GetLogger("ProcessLog");
        }

        public override void OnException(ExceptionContext context)
        {
            var result = new APIResponseDto
            {
                ResponseEnum = APIResponseEnum.Exception
            };

            _loggerProcess.Fatal(context.Exception);

            context.Result = new ObjectResult(result);
        }

    }
}
