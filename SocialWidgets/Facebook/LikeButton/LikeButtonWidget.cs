using Sitefinity.Widgets.Social.Facebook.Enumerations;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Social.Facebook
{
	/// <summary>
	/// This is the control that represents the Sitefinity implementation of the Facebook Like button
	/// </summary>
	[ControlDesigner(typeof(LikeButtonWidgetDesigner))]
	public class LikeButtonWidget : SimpleView
	{
		#region Constants
		const string IFRAME_SRC = "<iframe src=\"http://www.facebook.com/plugins/like.php?href={0}&amp;layout={1}&amp;show_faces={2}&amp;width={3}&amp;action={4}&amp;font={5}&amp;colorscheme={6}&amp;height={7}\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:{3}px; height:{7}px;\" allowTransparency=\"true\"></iframe>";
		const string FBML_SRC = "<fb:like href=\"{0}\" layout=\"{1}\" show_faces=\"{2}\" width=\"{3}\" action=\"{4}\" font=\"{5}\" colorscheme=\"{6}\"></fb:like>";
		#endregion
		
		#region Enumerations
		
		public enum LikeButtonLayoutStyle { Standard, Button_Count, Box_Count }

		public enum LikeButtonAction { Like, Recommend }
		
		#endregion

		#region Private Properties
		private WidgetModeType _widgetMode = WidgetModeType.IFRAME;

		private LikeButtonLayoutStyle _layout = LikeButtonLayoutStyle.Standard;
		private bool _showFaces = true;
		private int _width = 450;
		private LikeButtonAction _action = LikeButtonAction.Like;
		private string _font = "arial";
		private ColorSchemeType _colorScheme = ColorSchemeType.Light;
		private int _height = 80;
		
		#endregion

		#region Properties

		/// <summary>
		/// Gets the URL of the page to Like/Recommend
		/// </summary>
		/// <value>
		/// The Page Url
		/// </value>
		public string Url
		{
			get { return this.Page.Request.Url.AbsoluteUri; }
		}

		/// <summary>
		/// Gets or sets the width of the Facebook LikeButton
		/// </summary>
		/// <value>
		/// The width of the Facebook LikeButton
		/// </value>
		public int WidgetWidth
		{
			get { return _width; }
			set { _width = value; }
		}

		/// <summary>
		/// Gets or sets the height of the Facebook LikeButton
		/// </summary>
		/// <value>
		/// The height of the Facebook LikeButton
		/// </value>
		public int WidgetHeight
		{
			get { return _height; }
			set { _height = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show user faces
		/// </summary>
		/// <value>
		///   <c>true</c> if Facebook user faces should be shown; otherwise, <c>false</c>.
		/// </value>
		public bool ShowFaces
		{
			get { return _showFaces; }
			set { _showFaces = value; }
		}

		/// <summary>
		/// Gets or sets the color scheme for the Facebook LikeButton.
		/// </summary>
		/// <value>
		/// The color scheme of the Facebook LikeButton.
		/// </value>
		public ColorSchemeType ColorScheme
		{
			get { return _colorScheme; }
			set { _colorScheme = value; }
		}

		public LikeButtonLayoutStyle Layout
		{
			get { return _layout; }
			set { _layout = value; }
		}

		public LikeButtonAction Action
		{
			get { return _action; }
			set { _action = value; }
		}

		public string Font
		{
			get { return _font; }
			set { _font = value; }
		}

		/// <summary>
		/// Gets or sets the mode used to render the Facebook LikeButton.
		/// </summary>
		/// <value>
		/// The mode used to render the Facebook LikeButton.
		/// </value>
		public WidgetModeType WidgetMode
		{
			get { return _widgetMode; }
			set { _widgetMode = value; }
		}
		#endregion

		#region Control references

		/// <summary>
		/// Gets the reference to the literal in which the Facebook like tag will be rendered.
		/// </summary>
		protected Literal LikeButtonHtml
		{
			get { return this.Container.GetControl<Literal>("LikeButtonHtml", true); }
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
                return Resources.VirtualPathPrefix + "Sitefinity.Widgets.Social.Facebook.LikeButton.LikeButtonWidget.ascx";
            }

            set
            {
                base.LayoutTemplatePath = value;
            }
        }
		#endregion

		#region Public and overriden methods

		public HttpServerUtility Server = HttpContext.Current.Server;

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
			
			// append fb script
			if (WidgetMode == WidgetModeType.FBML) sb.Append(Constants.FBML_SCRIPT);
			
			// bind html to control
			if (WidgetMode == WidgetModeType.IFRAME)
				sb.AppendFormat(IFRAME_SRC, Server.UrlEncode(this.Url), this.Layout, this.ShowFaces, WidgetWidth, this.Action.ToString().ToLower(), Server.UrlEncode(this.Font.ToLower()), this.ColorScheme.ToString().ToLower(), WidgetHeight);
			else
				sb.AppendFormat(FBML_SRC, HttpContext.Current.Server.UrlEncode(this.Url), this.Layout, this.ShowFaces, WidgetWidth, this.Action.ToString().ToLower(), this.Font.ToLower(), this.ColorScheme.ToString().ToLower());
			LikeButtonHtml.Text = sb.ToString();
		}

		#endregion
	}
}
