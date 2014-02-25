using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Calendar.iCal.iCalFeed
{
	[ControlDesigner(typeof(Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner)), PropertyEditorTitle("iCal Feed")]
	public class iCalFeedWidget : SimpleView
	{
		#region Private Properties

		private string _feedUrl = "~/ical/feed";
		private string _linkText = "Subscribe to iCal Feed";

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the iCal feed link text.
		/// </summary>
		/// <value>
		/// The link text for the iCal feed.
		/// </value>
		public string LinkText
		{
			get { return _linkText; }
			set { _linkText = value; }
		}

		/// <summary>
		/// Gets or sets the URL to the iCal Feed.
		/// </summary>
		/// <value>
		/// The feed iCal Feed URL.
		/// </value>
		public string FeedUrl
		{
			get { return _feedUrl; }
			set { _feedUrl = value; }
		}

		#endregion

		#region Template Controls

		public virtual HyperLink FeedLink
		{
			get { return this.Container.GetControl<HyperLink>("FeedLink", true); }
		}

		#endregion

		protected override void InitializeControls(GenericContainer container){
			// TODO: Implement this method
			FeedLink.NavigateUrl = FeedUrl;
			FeedLink.Text = LinkText;
		}

		protected override string LayoutTemplateName
		{
			get
			{
				// TODO: Implement this property getter
				return string.Empty;
			}
		}
	}
}