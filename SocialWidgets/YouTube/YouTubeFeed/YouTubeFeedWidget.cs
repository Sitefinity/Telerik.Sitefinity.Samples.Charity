using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Social.YouTube
{
	/// <summary>
	/// Sitefinity Widget to show recent videos from a YouTube account
	/// </summary>
	[ControlDesigner(typeof(Sitefinity.Widgets.Social.YouTube.YouTubeFeedWidgetDesigner)), PropertyEditorTitle("YouTube Latest Videos")]
	public partial class YouTubeFeedWidget : SimpleView
	{
		#region Private Properties

		private string _username = "AmRedCross";
		private bool _showTitles = true;
		private int _maxVideos = 5;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the YouTube username.
		/// </summary>
		/// <value>
		/// The YouTube username.
		/// </value>
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		public bool ShowTitles
		{
			get { return _showTitles; }
			set { _showTitles = value; }
		}

		/// <summary>
		/// Gets or sets the max nnumber of videos to retrieve.
		/// </summary>
		/// <value>
		/// The max number of videos.
		/// </value>
		public int MaxVideos
		{
			get { return _maxVideos; }
			set { _maxVideos = value; }
		}

		#endregion

		#region Constants

		const string FEED_URL = "http://gdata.youtube.com/feeds/base/users/{0}/uploads?alt=rss";

		#endregion

		#region Template Controls

		protected virtual Repeater YouTubeRepeater
		{
			get { return this.Container.GetControl<Repeater>("YouTubeRepeater", true); }
		}

		/// <summary>
		/// Link to the users YouTube profile in the widget header
		/// </summary>
		protected virtual HyperLink HeaderLink
		{
			get { return this.Container.GetControl<HyperLink>("HeaderLink", true); }
		}

		/// <summary>
		/// Youtube Profile link in the Widget Footer
		/// </summary>
		protected virtual HyperLink FooterLink
		{
			get { return this.Container.GetControl<HyperLink>("FooterLink", true); }
		}

        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Override this property to change the embedded template to be used with the dialog
        /// </remarks>
		protected override string LayoutTemplateName
		{
			get { return null; }
		}

        /// <summary>
        /// Gets or sets the path of the external template to be used by the control.
        /// </summary>
        /// <value></value>
        public override string LayoutTemplatePath
        {
            get
            {
                return Resources.VirtualPathPrefix + "Sitefinity.Widgets.Social.YouTube.YouTubeFeed.YouTubeFeedWidget.ascx";
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
		#endregion

		protected override void InitializeControls(GenericContainer container)
		{

		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			EnsureChildControls();

			// bind links
			HeaderLink.Text = Username;
            HeaderLink.NavigateUrl = FooterLink.NavigateUrl = string.Concat("http://www.youtube.com/", Username);

			// ensure the username is valid
			if (string.IsNullOrEmpty(Username)) return;

			// retrieve contents
			var url = string.Format(FEED_URL, Username);
			var vidXml = XDocument.Load(url);
			if (vidXml == null) return;

			// parse to collection
			var query = from t in vidXml.Descendants("item") select new { Url = t.Element("link").Value, Title = t.Element("title").Value, Thumbnail = ToThumbnail(t.Element("link").Value) };

			// empty set?
			if (query.Count() == 0) return;

			// bind, limiting count
			YouTubeRepeater.ItemDataBound += new RepeaterItemEventHandler(YouTubeRepeater_ItemDataBound);
			YouTubeRepeater.DataSource = query.Take(MaxVideos);
			YouTubeRepeater.DataBind();
		}

		void YouTubeRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (!ShowTitles) return;

			// bind link
			var dataItem = e.Item.DataItem;
			var lnkTimeStamp = e.Item.FindControl("lnkTitle") as HyperLink;
			lnkTimeStamp.NavigateUrl = DataBinder.Eval(dataItem, "Url").ToString();
			lnkTimeStamp.Text = DataBinder.Eval(dataItem, "Title").ToString();
		}

		/// <summary>
		/// Retrieves a thumbnail for a youtube video link
		/// </summary>
		/// <param name="input">The url to the video.</param>
		/// <returns></returns>
		protected string ToThumbnail(string input)
		{
			// retrieve url
			var uri = new Uri(input);

			// parse to querystring
			var qs = HttpUtility.ParseQueryString(uri.Query);
			var vidID = qs["v"];

			// create thumbnail from qs
			return string.IsNullOrEmpty(vidID) ? string.Empty : string.Format("http://img.youtube.com/vi/{0}/2.jpg", vidID);
		}
	}
}