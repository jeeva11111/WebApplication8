using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication8.Filters
{
    public class NotifactionFIlter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {

        
            base.OnActionExecuted(context);
        }

    }

    // public class JsonMessage 
}