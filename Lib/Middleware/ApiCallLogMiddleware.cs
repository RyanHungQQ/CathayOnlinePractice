using Microsoft.AspNetCore.Http;
using NLog;

namespace Lib.Middleware
{
    public class ApiCallLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ApiCallLogMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetLogger("apilog");  // 使用 api 日誌記錄
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            // 記錄請求
            httpContext.Request.EnableBuffering();  // 使請求流可重讀

            var request = httpContext.Request;
            var body = request.Body;

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            _logger.Info($"Request: {request.Method} {request.Path} {requestBody}");  // 記錄請求

            request.Body.Seek(0, SeekOrigin.Begin);  // 重置流的位置

            // 記錄回應
            var originalBodyStream = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                // 將 Response.Body 指向 MemoryStream
                httpContext.Response.Body = responseBody;

                // 執行後續的操作，調用後續中介軟體或控制器
                await _next(httpContext);

                // 讀取並記錄回應 body
                responseBody.Seek(0, SeekOrigin.Begin);
                string responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();
                _logger.Info($"Response: {httpContext.Response.StatusCode} {responseBodyContent}");  // 記錄回應

                // 重置 responseBody 流的位置，確保它可以將內容寫回到原始的 Response.Body
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);  // 寫回原始的回應流
            }
        }
    }
}
