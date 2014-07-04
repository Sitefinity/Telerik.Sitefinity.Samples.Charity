using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Social.Twitter
{
	[RequireScriptManager]
	[ControlDesigner(typeof(Sitefinity.Widgets.Social.Twitter.TwitterFeedWidgetDesigner)), PropertyEditorTitle("Twitter Feed")]
	public class TwitterFeedWidget : SimpleView
	{
		#region Private Properties

		private string _username = "Sitefinity";
		private int _maxTweets = 5;
		private Unit _width = new Unit("240px");
		private Unit _height = new Unit("400px");
		private bool _showTimeStamp = false;

		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the twitter username.
		/// </summary>
		/// <value>
		/// The twitter username.
		/// </value>
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		/// <summary>
		/// Gets or sets the number of tweets to retreive.
		/// </summary>
		/// <value>
		/// The number tweets to retreive.
		/// </value>
		public int MaxTweets
		{
			get { return _maxTweets; }
			set { _maxTweets = value; }
		}

		/// <summary>
		/// Gets or sets the width of the widget container.
		/// </summary>
		/// <value>
		/// The width of the widget container.
		/// </value>
		public override Unit Width
		{
			get { return _width; }
			set { _width = value; }
		}

		/// <summary>
		/// Gets or sets the height of the widget container.
		/// </summary>
		/// <value>
		/// The height of the widget container.
		/// </value>
		public override Unit Height
		{
			get { return _height; }
			set { _height = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show twitter time stamp.
		/// </summary>
		/// <value>
		///   <c>true</c> if time stamp should be shown; otherwise, <c>false</c>.
		/// </value>
		public bool ShowTimeStamp
		{
			get { return _showTimeStamp; }
			set { _showTimeStamp = value; }
		}

		#endregion

		#region Constants

		const string FEED_URL = "http://twitter.com/statuses/user_timeline/{0}.rss?count={1}";

		private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.IgnoreCase;

		/// <summary>
		/// Jon Gruber's URL Regex: http://daringfireball.net/2009/11/liberal_regex_for_matching_urls
		/// </summary>
		private static readonly Regex _parseUrls = new Regex(@"\b(([\w-]+://?|www[.])[^\s()<>]+(?:\([\w\d]+\)|([^\p{P}\s]|/)))", Options);

		/// <summary>
		/// Diego Sevilla's @ (mention) Regex: http://stackoverflow.com/questions/529965/how-could-i-combine-these-regex-rules
		/// </summary>
		private static readonly Regex _parseMentions = new Regex(@"(^|\W)@([A-Za-z0-9_]+)", Options);

		/// <summary>
		/// Simon Whatley's # (hashtag) Regex: http://www.simonwhatley.co.uk/parsing-twitter-usernames-hashtags-and-urls-with-javascript
		/// </summary>
		private static readonly Regex _parseHashtags = new Regex("[#]+[A-Za-z0-9-_]+", Options);

		/// <summary>
		/// Resource name for the Twitter logo image
		/// </summary>
		private static readonly string TWITTER_LOGO_RESOURCE = "Sitefinity.Widgets.Social.Resources.Images.twitter-logo.png";

		#endregion

		#region Template Controls

		/// <summary>
		/// The Repeater control to render tweets on the page.
		/// </summary>
		protected virtual Repeater TwitterRepeater
		{
			get { return this.Container.GetControl<Repeater>("TwitterRepeater", true); }
		}

		/// <summary>
		/// Image control for the User Profile Icon
		/// </summary>
		protected virtual Image TwitterProfileIcon
		{
			get { return this.Container.GetControl<Image>("TwitterProfileIcon", true); }
		}

		/// <summary>
		/// Link to the users twitter profile in the widget header
		/// </summary>
		protected virtual HyperLink HeaderLink
		{
			get { return this.Container.GetControl<HyperLink>("HeaderLink", true); }
		}

		/// <summary>
		/// "Join the Conversation" link in the Widget Footer
		/// </summary>
		protected virtual HyperLink FooterLink
		{
			get { return this.Container.GetControl<HyperLink>("FooterLink", true); }
		}

		/// <summary>
		/// Image control for showing the Twitter logo
		/// </summary>
		protected virtual Image TwitterLogo
		{
			get { return this.Container.GetControl<Image>("TwitterLogo", true); }
		}

		/// <summary>
		/// Gets the name of the layout template.
		/// </summary>
		/// <value>
		/// The name of the layout template.
		/// </value>
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
                return Resources.VirtualPathPrefix + "Sitefinity.Widgets.Social.Twitter.TwitterFeed.TwitterFeedWidget.ascx";
            }

            set
            {
                base.LayoutTemplatePath = value;
            }
        }

		/// <summary>
		/// Initializes the controls.
		/// </summary>
		/// <param name="container">The container.</param>
		protected override void InitializeControls(GenericContainer container)
		{
		}

		#endregion

		/// <summary>
		/// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			EnsureChildControls();

			// ensure the username is valid
			if (string.IsNullOrEmpty(Username)) return;

			// bind twitter icon
			TwitterProfileIcon.ImageUrl = string.Format("http://img.tweetimag.es/i/{0}_m", Username);

			// bind links
			HeaderLink.Text = Username;
			HeaderLink.NavigateUrl = FooterLink.NavigateUrl = string.Concat("http://twitter.com/", Username);

			// bind logo
			TwitterLogo.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), TWITTER_LOGO_RESOURCE);

			// retrieve contents
			var url = string.Format(FEED_URL, Username, MaxTweets);
			var twitterXML = XDocument.Load(url);
			if (twitterXML == null) return;

			// parse to collection
			var query = from t in twitterXML.Descendants("item") select new { TweetID = ExtractGuid(t.Element("guid").Value), Tweet = ToHtml(t.Element("title").Value), TimeStamp = ToDate(t.Element("pubDate").Value), Url = t.Element("link").Value };

			// bind if not empty
			if (query.Count() == 0) return;

			// bind
			TwitterRepeater.ItemDataBound += new RepeaterItemEventHandler(TwitterRepeater_ItemDataBound);
			TwitterRepeater.DataSource = query;
			TwitterRepeater.DataBind();
		}

		/// <summary>
		/// Handles the ItemDataBound event of the TwitterRepeater control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
		void TwitterRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var dataItem = e.Item.DataItem;

			// Timestamp
			if (ShowTimeStamp)
			{
				// bind link
				var lnkTimeStamp = e.Item.FindControl("lnkTimeStamp") as HyperLink;
				lnkTimeStamp.NavigateUrl = DataBinder.Eval(dataItem, "Url").ToString();
				lnkTimeStamp.Text = DataBinder.Eval(dataItem, "TimeStamp").ToString();

				// show separator
				var separator = e.Item.FindControl("Separator") as Literal;
				separator.Visible = true;
			}

			// bind reply link
			var lnkReply = e.Item.FindControl("lnkReply") as HyperLink;
			lnkReply.NavigateUrl = string.Format("http://twitter.com/?status=@{0}%20&in_reply_to_status_id={1}&in_reply_to={0}", Username, DataBinder.Eval(e.Item.DataItem, "TweetID"));
		}


		/// <summary>
		/// Extracts a status tweet id from the Tweet Url (Guid Rss Element).
		/// </summary>
		/// <param name="input">The tweet status Url.</param>
		/// <returns>String containing the status tweet ID</returns>
		protected string ExtractGuid(string input)
		{
			// empty string
			if (string.IsNullOrEmpty(input)) { return string.Empty; }

			// split url
			var url = input.Split('/');
			if (url.Length == 0) return string.Empty;

			// twitter status id is the last element in the Uri
			return url[url.Length - 1];
		}

		/// <summary>
		/// Converts the string date to a DateTime
		/// </summary>
		/// <param name="input">The input string.</param>
		/// <returns>DateTime of the timestamp</returns>
		protected string ToDate(string input)
		{
			// empty string
			if (string.IsNullOrEmpty(input)) { return string.Empty; }

			// Parse Date
			DateTime d;
			if (!DateTime.TryParse(input, out d)) return string.Empty;

			// determine age of tweet
			var ts = DateTime.Now.Subtract(d);
			if (ts.Days >= 1)
				return string.Concat(ts.Days, " Days Ago");

			if (ts.Hours >= 1)
				return string.Concat(ts.Hours, " Hours Ago");

			return string.Concat(ts.Minutes, " Minutes Ago");
		}

		/// <summary>
		/// Linkified hashtags, usernames, and links in the text.
		/// </summary>
		/// <param name="input">The input text to be linkified.</param>
		/// <returns></returns>
		protected string ToHtml(string input)
		{
			// empty string
			if (string.IsNullOrEmpty(input)) { return input; }

			// match URLs
			foreach (Match match in _parseUrls.Matches(input))
			{
				var url = match.Value.StartsWith("http") ? match.Value : string.Concat("http://", match.Value);
				input = input.Replace(match.Value, string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", url));
			}

			// match @ mentinos
			foreach (Match match in _parseMentions.Matches(input))
			{
				if (match.Groups.Count != 3)
				{
					continue;
				}

				var screenName = match.Groups[2].Value;
				var mention = "@" + screenName;

				input = input.Replace(mention, string.Format("<a href=\"http://twitter.com/{0}\" target=\"_blank\">{1}</a>", screenName, match.Value));
			}

			// match # hashtags
			foreach (Match match in _parseHashtags.Matches(input))
			{
				var hashtag = HttpContext.Current.Server.UrlEncode(match.Value);
				input = input.Replace(match.Value, string.Format("<a href=\"http://search.twitter.com/search?q={0}\" target=\"_blank\">{1}</a>", hashtag, match.Value));
			}

			return input;
		}
	}
}