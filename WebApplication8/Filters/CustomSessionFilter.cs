using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication8.Filters
{
    public class CustomSessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var session = context.HttpContext.Session.GetString("");
            base.OnActionExecuting(context);
        }
    }
}
