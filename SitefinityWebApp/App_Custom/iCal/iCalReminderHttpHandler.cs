using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using Telerik.Sitefinity;

namespace SitefinityWebApp.App_Custom.iCal
{
    public class iCalReminderHttpHandler : IHttpHandler
    {
        public iCalReminderHttpHandler(Guid id)
        {
            this.eventID = id;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var host = context.Request.Url.Host;

            // initialize calendar item
            var cal = new iCalendar();
            cal.Version = "1.0";

            // find the event
            Telerik.Sitefinity.Events.Model.Event ev;
            using (var fluent = App.WorkWith())
            {
                // TODO: replace this with correct logic for retrieving event
                ev = fluent.Events().Publihed().Get().FirstOrDefault();
            }

            if (ev == null) return;

            var appt = new DDay.iCal.Event();
            appt.Start = new iCalDateTime(ev.EventStart.ToUniversalTime());
            if (ev.EventEnd.HasValue)
                appt.End = new iCalDateTime(ev.EventEnd.Value.ToUniversalTime());
            else
                appt.IsAllDay = true;

            // save and format description
            var description = ev.Content.ToString().Replace("\r\n", "<br /><br/>");
            description = description.Replace("src=\"/", "src=\"http://" + host + "/");
            appt.AddProperty("X-ALT-DESC", description);

            // non-html property
            var reg = new Regex("<(.|\n)+?>");
            appt.Description = reg.Replace(description, string.Empty);

            // event location
            var location = ev.Street;

            appt.Location = location.ToString();

            appt.Summary = ev.Title;

            // url
            cal.Events.Add(appt);

            // write calendar feed!
            var ser = new iCalendarSerializer(cal);
            context.Response.Clear();
            context.Response.ContentType = "text/calendar";
            context.Response.AddHeader("content-disposition", "attachment; filename=Calendar.ics");
            context.Response.Write(ser.SerializeToString());
        }

        private Guid eventID;
    }
}