
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication8.Data;
using WebApplication8.Models.Notify;
using Microsoft.AspNetCore.Http;
using System;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using WebApplication8.Models.Video;

namespace WebApplication8.Filters
{
    public class NotificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var httpContextAccessor = context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;

            if (dbContext != null && httpContextAccessor != null)
            {
                var userId = httpContextAccessor.HttpContext.Session.GetString("UserId");
                if (int.TryParse(userId, out int currentUser))
                {
                    var videoTitle = context.HttpContext.Items["VideoTitle"] as string;
                    var videoDescription = context.HttpContext.Items["VideoDescription"] as string;
                    var ChennelId = Convert.ToInt32((context.HttpContext.Items["ChennelId"]));
                    //  var ChennelId = context.HttpContext.Session.GetInt32("ChennelId");
                    var newNotification = new Notify
                    {
                        IsActive = true,
                        Message = $"New video posted: {videoDescription}", // Use description as message
                        Status = 1,
                        Title = videoTitle,
                        UserId = currentUser,
                        ChennelId = ChennelId
                    };

                    dbContext.Notifys.Add(newNotification);
                    dbContext.SaveChanges();
                }
            }


            base.OnActionExecuted(context);
        }
    }
}


public class VideoPostNotifactionMessage : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var _context = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
        var _accessor = context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;


        if (_accessor != null && _context != null && context.Result != null)
        {
            var userId = _accessor.HttpContext.Session.GetString("UserId");

            if (int.TryParse(userId, out int currentUser))
            {
                // Assuming you want to fetch notifications for the current user and not just the last one from the whole table
                var notificationsForUser = _context.Videos
                                                    .Where(n => n.Id == currentUser)
                                                     .OrderBy(n => n.Id);

                // You might want to return all notifications for the user, not just the last one

                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Chennel",
                    action = "Index"
                }));

                // context.Result = new ObjectResult(new object[] { notificationsForUser });
            }
            else
            {
                // Handle case where userId is not available/parseable
                context.Result = new ObjectResult(new { Error = "videos not found or not logged in." });
            }

        }

        base.OnActionExecuted(context);
    }
    public class GetChennelInfo : ActionFilterAttribute
    {


        public override void OnActionExecuted(ActionExecutedContext context)
        {

            var currentChennel = context.HttpContext.RequestServices.GetService(typeof(Chennel)) as Chennel;


            base.OnActionExecuted(context);
        }
    }
}




