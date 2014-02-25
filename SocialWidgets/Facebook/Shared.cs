namespace Sitefinity.Widgets.Social.Facebook.Enumerations
{
	/// <summary>
	/// Enumeration to determine the Color Scheme for the Facebook LikeBox (Light or Dark)
	/// </summary>
	public enum ColorSchemeType { Light, Dark }

	/// <summary>
	/// Enumeration to determine what method to use to render the Facebook LikeBox (IFRAME or FBML)
	/// </summary>
	public enum WidgetModeType { IFRAME, FBML }

}

namespace Sitefinity.Widgets.Social.Facebook
{
	public static class Constants
	{
		public static readonly string FBML_SCRIPT = "<script src=\"http://connect.facebook.net/en_US/all.js#xfbml=1\"></script>";
	}
}