using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication8.Filters
{
	public class FetcherCustom : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
		
			
			base.OnActionExecuting(context);
		}
	}
}
