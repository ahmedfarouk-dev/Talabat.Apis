using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Talabat.Apis.ActionFilters
{
    public class ValidationActionFilter : Attribute, IExceptionFilter
    {


        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey("id"))
            {
                context.Result = new BadRequestObjectResult("ID is required");
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("ok");
        }

        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
