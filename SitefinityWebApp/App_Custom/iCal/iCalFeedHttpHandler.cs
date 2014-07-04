using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity;

namespace SitefinityWebApp.App_Custom.iCal
{
	/// <summary>
	/// HttpHandler to generate an iCal Feed
	/// </summary>
	public class iCalFeedHttpHandler : IHttpHandler
	{
		/// <summary>
		/// The current year
		/// </summary>
		private int year;

		/// <summary>
		/// The current month
		/// </summary>
		private int month;

		/// <summary>
		/// Initializes a new instance of the <see cref="iCalFeedHttpHandler"/> class.
		/// </summary>
		/// <param name="Year">The year.</param>
		/// <param name="Month">The month.</param>
		public iCalFeedHttpHandler(int Year, int Month)
		{
			this.year = Year;
			this.month = Month;
		}

		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
		/// </summary>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
		public bool IsReusable
		{
			get { return false; }
		}

		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
		/// </summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext context)
		{
			var host = context.Request.Url.Host;

			// TODO: Support Filtering by date/month

			// TODO: Support filtering by category

			// initializie calendar
			var cal = new iCalendar();
			cal.Version = "1.0";

			// get events +/- 6 months
			IQueryable events;
			using (var fluent = App.WorkWith())
			{
				var center = new DateTime(year, month, 1);
				var start = center.AddMonths(-6);
				var end = center.AddMonths(6);

				events = fluent.Events().Where(e => e.EventStart >= start && e.EventEnd <= end && e.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live).Get();
			}

			foreach (Telerik.Sitefinity.Events.Model.Event ev in events)
			{
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
				appt.Description = reg.Replace(description, "");

				// event location
				var location = ev.Street;
			  
                appt.Location = location.ToString();

				appt.Summary = ev.Title;

				appt.Url = new Uri("http://www.bing.com");
				cal.Events.Add(appt);
			}

			// write calendar feed!
			var ser = new iCalendarSerializer(cal);
			context.Response.Clear();
			context.Response.ContentType = "text/calendar";
			context.Response.AddHeader("content-disposition", "attachment; filename=Calendar.ics");
			context.Response.Write(ser.SerializeToString());
			//context.Response.Flush();
		}
	}
}