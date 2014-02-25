using System;
using System.Web;
using System.Web.Routing;

namespace SitefinityWebApp.App_Custom.iCal
{
    public class iCalRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var eventID = (string)requestContext.RouteData.Values["id"];
            if (string.IsNullOrEmpty(eventID))
                return new iCalFeedHttpHandler(DateTime.Now.Year, DateTime.Now.Month);
            else
                return new iCalReminderHttpHandler(new Guid(eventID));
        }
    }
}