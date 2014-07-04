using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Sitefinity.Widgets.Social.Facebook.Enumerations;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Social.Facebook
{
	[ControlDesigner(typeof(Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner)), PropertyEditorTitle("Facebook Like Box")]
	public partial class LikeBoxWidget : SimpleView
	{
		#region Constants
		public const string IFRAME_SRC = "<iframe src=\"http://www.facebook.com/plugins/likebox.php?href={0}&amp;width={1}&amp;colorscheme={2}&amp;connections={3}&amp;stream={4}&amp;header={5}&amp;height={6}\" height=\"{6}\" width=\"{1}\" scrolling=\"no\" frameborder=\"0\" style=\"border: none; overflow: hidden; width: {1}px; height: {6}px;\"></iframe>";
		public const string FBML_SCRIPT = "<script src=\"http://connect.facebook.net/en_US/all.js#xfbml=1\"></script>";
		public const string FBML_SRC = "<fb:like-box href=\"{0}\" width=\"{1}\" colorscheme=\"{2}\" connections=\"{3}\" stream=\"{4}\" header=\"{5}\" height=\"{6}\"></fb:like-box>";
		#endregion

		#region Private Properties
		private string _url = "http://www.facebook.com/sitefinity";
		private int _width = 292;
		private int _height = 587;
		private int _numConnections = 10;
		private bool _showHeader = true;
		private ColorSchemeType _colorScheme = ColorSchemeType.Dark;
		private WidgetModeType _widgetMode = WidgetModeType.IFRAME;
		private bool _showStream = true;
		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the URL to the Facebook Page
		/// </summary>
		/// <value>
		/// The URL to the Facebook Page
		/// </value>
		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		/// <summary>
		/// Gets or sets the width of the Facebook LikeBox
		/// </summary>
		/// <value>
		/// The width of the Facebook LikeBox
		/// </value>
		public int WidgetWidth
		{
			get { return _width; }
			set { _width = value; }
		}

		/// <summary>
		/// Gets or sets the height of the Facebook LikeBox
		/// </summary>
		/// <value>
		/// The height of the Facebook LikeBox
		/// </value>
		public int WidgetHeight
		{
			get { return _height; }
			set { _height = value; }
		}

		/// <summary>
		/// Gets or sets the number of connections to show in the Facebook LikeBox.
		/// </summary>
		/// <value>
		/// The number of connections to show in the Facebook LikeBox. Entor 0 to hide the Connections section.
		/// </value>
		public int NumConnections
		{
			get { return _numConnections; }
			set { _numConnections = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the Facebook Page Stream.
		/// </summary>
		/// <value>
		///   <c>true</c> if Facebook Page Stream should be shown; otherwise, <c>false</c>.
		/// </value>
		public bool ShowStream
		{
			get { return _showStream; }
			set { _showStream = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the Facebook LikeBox header.
		/// </summary>
		/// <value>
		///   <c>true</c> if Facebook LikeBox header should be shown; otherwise, <c>false</c>.
		/// </value>
		public bool ShowHeader
		{
			get { return _showHeader; }
			set { _showHeader = value; }
		}

		/// <summary>
		/// Gets or sets the color scheme for the Facebook LikeBox.
		/// </summary>
		/// <value>
		/// The color scheme of the Facebook LikeBox.
		/// </value>
		public ColorSchemeType ColorScheme
		{
			get { return _colorScheme; }
			set { _colorScheme = value; }
		}

		/// <summary>
		/// Gets or sets the mode used to render the Facebook LikeBox.
		/// </summary>
		/// <value>
		/// The mode used to render the Facebook LikeBox.
		/// </value>
		public WidgetModeType WidgetMode
		{
			get { return _widgetMode; }
			set { _widgetMode = value; }
		}

		#endregion

		#region Template Controls

		protected virtual Literal LikeBoxHtml
		{
			get { return this.Container.GetControl<Literal>("LikeBoxHtml", true); }
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
                return Resources.VirtualPathPrefix + "Sitefinity.Widgets.Social.Facebook.LikeBox.LikeBoxWidget.ascx";
            }

            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
		protected override void InitializeControls(GenericContainer container)
		{
			var sb = new StringBuilder();
			if (WidgetMode == WidgetModeType.FBML) sb.Append(Constants.FBML_SCRIPT);
			sb.AppendFormat(WidgetMode == WidgetModeType.IFRAME ? IFRAME_SRC : FBML_SRC, HttpContext.Current.Server.UrlEncode(Url), WidgetWidth, ColorScheme.ToString().ToLower(), NumConnections, ShowStream, ShowHeader, WidgetHeight);
			LikeBoxHtml.Text = sb.ToString();
		}

		#endregion
	}
}