using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Modules.Events.Web.UI.Public;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;	

namespace SitefinityWebApp.Widgets.Calendar
{
	/// <summary>
	/// Webcontrol for showing a Calendar of events
	/// </summary>
	public class CalendarView : SimpleView
	{
		#region Private Members

		private Unit _width = new Unit("100%");
		private Unit _height = new Unit("400px");
		private int _maxItems = 4;
		private string _template = "~/Widgets/Calendar/CalendarViewTemplate.ascx";

		ILookup<int, Event> Month_Events;
		protected DateTime SelectedDate;

		#endregion

		#region Public Members

		/// <summary>
		/// Gets or sets the url to the details page.
		/// </summary>
		/// <value>
		/// The details page url.
		/// </value>
		public string DetailsPage { get; set; }
		
		/// <summary>
		/// Gets or sets the width of the Web server control.
		/// </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit"/> that represents the width of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty"/>.</returns>
		///   
		/// <exception cref="T:System.ArgumentException">The width of the Web server control was set to a negative value. </exception>
		public override Unit Width
		{
			get { return _width; }
			set { _width = value; }
		}

		/// <summary>
		/// Gets or sets the height of the Web server control.
		/// </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit"/> that represents the height of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty"/>.</returns>
		///   
		/// <exception cref="T:System.ArgumentException">The height was set to a negative value.</exception>
		public override Unit Height
		{
			get { return _height; }
			set { _height = value; }
		}

		/// <summary>
		/// Gets or sets the max number of events to show in a day cell.
		/// </summary>
		/// <value>
		/// The max number of events per day.
		/// </value>
		public int MaxItems
		{
			get { return _maxItems; }
			set { _maxItems = value; }
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Gets or sets the layout template path.
		/// </summary>
		/// <value>
		/// The layout template path.
		/// </value>
		public override string LayoutTemplatePath
		{
			get { return _template; }
			set { _template = value; }
		}

		/// <summary>
		/// Gets the name of the layout template.
		/// </summary>
		/// <value>
		/// The name of the layout template.
		/// </value>
		protected override string LayoutTemplateName
		{
			get { return "Widgets.Calendar.CalendarViewTemplate.ascx"; }
		}

		/// <summary>
		/// Gets the event calendar control.
		/// </summary> 
		protected virtual RadCalendar EventCalendar
		{
			get
			{
				return base.Container.GetControl<RadCalendar>("EventCalendar", true);
			}
		}

		#endregion

		/// <summary>
		/// Determines the url to the event details page
		/// </summary>
		/// <returns>The Details Page url</returns>
		private string GetDetailsPage()
		{
			// retrieve the Details Page ID from the parent
			var view = Parent.Parent as MasterView;
			if (view == null || view.MasterViewDefinition.DetailsPageId == Guid.Empty) return string.Empty;

			// retrieve page using fluent api
			using (var fluent = App.WorkWith())
			{
				// retrieve page using ID
				var page = fluent.Page(view.MasterViewDefinition.DetailsPageId).Get();
				if (page == null) return string.Empty;

				// format url
				return string.Concat("~/", page.UrlName);
			}
		}

		/// <summary>
		/// Initializes the controls.
		/// </summary>
		/// <param name="container">The container.</param>
		protected override void InitializeControls(GenericContainer container)
		{
			// defaults
			EventCalendar.Width = _width;
			EventCalendar.Height = _height;

			// attach event handlers
			EventCalendar.DayRender += Cal_DayRender;
			EventCalendar.DefaultViewChanged += Cal_DefaultViewChanged;

			// retrieve details page url
			DetailsPage = GetDetailsPage();

			// empty page means use the current page
			if (string.IsNullOrEmpty(DetailsPage)) DetailsPage = HttpContext.Current.Request.RawUrl;

			if (Page.IsPostBack) return;

			// init calendar
			SelectedDate = EventCalendar.FocusedDate;
			LoadData();
		}

		/// <summary>
		/// Populates the events collection from the API
		/// </summary>
		protected void LoadData()
		{
			IQueryable<Event> events;

			// retrive from fluent api
			using (var fluent = App.WorkWith())
			{
				// range for current month only
				var start = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
				var end = new DateTime(SelectedDate.Year, SelectedDate.Month, DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month));

				// get events
				events = fluent.Events().Where(e => e.EventStart >= start && e.EventEnd <= end && e.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live).Get();
			}
			
			// empty set?
			if (events.Count() == 0) return;

			// parse to collection
			Month_Events = events.ToLookup(e => e.EventStart.Day);
		}

		/// <summary>
		/// Handles the DayRender event of the Cal control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="Telerik.Web.UI.Calendar.DayRenderEventArgs"/> instance containing the event data.</param>
		protected void Cal_DayRender(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
		{
			// skip other month days
			if (e.Day.Date.Month != SelectedDate.Month) return;

			// no events?
			if (Month_Events == null || Month_Events[e.Day.Date.Day].Count() == 0) return;

			// build links to events
			var sb = new StringBuilder();
			sb.Append("<ul>");

			// parse events
			foreach (var ev in Month_Events[e.Day.Date.Day])
			{
				// details page set?
				if (string.IsNullOrEmpty(DetailsPage))
					sb.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", ev.Urls[0].Url, ev.Title); // use current page
				else
					sb.AppendFormat("<li><a href=\"{0}{1}\">{2}</a></li>", ResolveUrl(DetailsPage), ev.Urls[0].Url, ev.Title); // use details page
			}

			sb.Append("</ul>");
			
			// write html
			e.Cell.Text += sb.ToString();
		}

		protected void Cal_DefaultViewChanged(object sender, Telerik.Web.UI.Calendar.DefaultViewChangedEventArgs e)
		{
			// select the new date
			SelectedDate = e.NewView.ParentCalendar.FocusedDate;
			EventCalendar.SelectedDate = EventCalendar.FocusedDate = SelectedDate;

			LoadData();
		}
	}
}