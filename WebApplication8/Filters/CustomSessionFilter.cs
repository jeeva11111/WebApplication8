using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication8.Filters
{
    public class CustomSessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var session = context.HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(session))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login"
                }));
            }
           
            base.OnActionExecuting(context);
        }
    }
}
