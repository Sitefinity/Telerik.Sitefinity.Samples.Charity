using System;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace SitefinityWebApp.Widgets.Donations
{
	/// <summary>
	/// Sitefinity Widget to accept donations via PayPal
	/// </summary>
	[ControlDesigner(typeof(Widgets.Donations.DonationWidgetDesigner)), PropertyEditorTitle("Donation")]
	public partial class DonationWidget : System.Web.UI.UserControl
	{
		#region Public Properties
		
		public string PaypalEmail { get; set; }

		public string OrganizationName { get; set; }

		public bool ShowCreditCardLogos { get; set; }

		#endregion

		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			btnSubmit.ImageUrl = ShowCreditCardLogos ? "https://www.paypal.com/en_US/i/btn/btn_donateCC_LG.gif" : "https://www.paypal.com/en_US/i/btn/btn_donate_LG.gif";
		}		
	}
}
