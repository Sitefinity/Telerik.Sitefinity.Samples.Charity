using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Social.Flickr
{
	/// <summary>
	/// Sitefinity widget to display recent images from a Flickr account
	/// </summary>
	[ControlDesigner(typeof(Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner)), PropertyEditorTitle("Flickr Feed")]
	public partial class FlickrFeedWidget : SimpleView
	{
		#region Private Properties

		private string _userID = "57076622@N02";
		private string _username = "sitefinity";
		private bool _showTitles = true;
		private int _maxPhotos = 10;

		#endregion

		#region Public Properies

		/// <summary>
		/// Gets or sets the Flickr user ID.
		/// </summary>
		/// <value>
		/// The Flickr user ID.
		/// </value>
		public string UserID
		{
			get { return _userID; }
			set { _userID = value; }
		}

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
		/// Gets or sets the max number photos to show.
		/// </summary>
		/// <value>
		/// The max number of photos.
		/// </value>
		public int MaxPhotos
		{
			get { return _maxPhotos; }
			set { _maxPhotos = value; }
		}

		/// <summary>
		/// Gets or sets the flickr tags to match/search.
		/// </summary>
		/// <value>
		/// The Flickr tags.
		/// </value>
		public string Tags { get; set; }

		#endregion

		#region Constants

		const string FEED_URL = "http://api.flickr.com/services/feeds/photos_public.gne?id={0}&lang=en-us&format=rss_200";

		#endregion

		#region Template Controls

		protected virtual Repeater FlickrRepeater
		{
			get { return this.Container.GetControl<Repeater>("FlickrRepeater", true); }
		}

		/// <summary>
		/// Link to the users flickr profile in the widget header
		/// </summary>
		protected virtual HyperLink HeaderLink
		{
			get { return this.Container.GetControl<HyperLink>("HeaderLink", true); }
		}

		/// <summary>
		/// Flickr Profile link in the Widget Footer
		/// </summary>
		protected virtual HyperLink FooterLink
		{
			get { return this.Container.GetControl<HyperLink>("FooterLink", true); }
		}

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
                return Resources.VirtualPathPrefix + "Sitefinity.Widgets.Social.Flickr.FlickrFeed.FlickrFeedWidget.ascx";
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
			HeaderLink.NavigateUrl = FooterLink.NavigateUrl = string.Concat("http://flickr.com/photos/", Username);

			// ensure the username is valid
			if (string.IsNullOrEmpty(UserID)) return;

			// retrieve contents
			var url = string.Format(FEED_URL, UserID);
			var flickrXml = XDocument.Load(url);
			if (flickrXml == null) return;

			// parse to collection
			XNamespace media = "http://search.yahoo.com/mrss/";
			var query = from t in flickrXml.Descendants("item") select new { Thumbnail = t.Element(media + "thumbnail").Attribute("url").Value, Url = t.Element("link").Value, Title = t.Element("title").Value };

			// empty result?
			if (query.Count() == 0) return;

			// bind, limiting count
			FlickrRepeater.ItemDataBound += new RepeaterItemEventHandler(FlickrRepeater_ItemDataBound);
			FlickrRepeater.DataSource = query.Take(MaxPhotos);
			FlickrRepeater.DataBind();
		}

		/// <summary>
		/// Handles the ItemDataBound event of the FlickrRepeater control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
		void FlickrRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (!ShowTitles) return;

			// bind link
			var dataItem = e.Item.DataItem;
			var lnkTimeStamp = e.Item.FindControl("lnkTitle") as HyperLink;
			lnkTimeStamp.NavigateUrl = DataBinder.Eval(dataItem, "Url").ToString();
			lnkTimeStamp.Text = DataBinder.Eval(dataItem, "Title").ToString();
		}


	}
}