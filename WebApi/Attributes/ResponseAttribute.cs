using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.CathayOnlinePractice.Response;
using static Common.Enums.WebApiEnum;

namespace WebApi.Attributes
{
    public class ResponseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                return;
            }
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            bool passCheck = IsDefinedOnController(controllerActionDescriptor, typeof(IgnoreResponseAttribute))
                                   || IsDefinedOnAction(controllerActionDescriptor, typeof(IgnoreResponseAttribute));
            if (passCheck)
            {
                return;
            }

            var objectContent = context.Result as ObjectResult;
            string typeName = objectContent?.Value?.GetType().BaseType.Name ?? "";
            if (typeName != typeof(ApiResponseBaseDto).Name)
            {
                var result = new APIResponseDto<object>
                {
                    ResponseEnum = APIResponseEnum.Success,
                    OutData = objectContent?.Value
                };
                context.Result = new ObjectResult(result);
            }

            base.OnActionExecuted(context);
        }
        private bool IsDefinedOnController(ControllerActionDescriptor controllerActionDescriptor, Type attributeType)
        {
            // Check if the attribute exists on the controller
            return controllerActionDescriptor.ControllerTypeInfo?.GetCustomAttributes(attributeType, true)?.Any() ?? false;
        }
        private bool IsDefinedOnAction(ControllerActionDescriptor controllerActionDescriptor, Type type)
        {
            // Check if the attribute exists on the action method
            return controllerActionDescriptor.MethodInfo?.GetCustomAttributes(inherit: true)?.Any(a => a.GetType() == type) ?? false;
        }
    }
}
