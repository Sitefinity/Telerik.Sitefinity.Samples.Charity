using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data.Summary;

namespace SitefinityWebApp.Tools
{
	public class Truncate : Literal
	{

		/// <summary>
		/// Determines how many words the Text will be truncated to.  Sitefinity will preserve the HTML.
		/// </summary>
		public int WordLength
		{
			get
			{
				return _wordlength;
			}

			set
			{
				_wordlength = value;
			}
		}

		/// <summary>
		/// This is used for backwards compatibility.  The first version of Truncate only had the Length property.
		/// </summary>
		public int Length
		{
			get
			{
				return WordLength;
			}

			set
			{
				WordLength = value;
			}
		}

		protected override void CreateChildControls()
		{
			var settings = new SummarySettings(SummaryMode.Words, WordLength, false);
			Text = SummaryParser.GetSummary(Text, settings);
			base.CreateChildControls();
		}

		private int _wordlength = 20;
	}
}