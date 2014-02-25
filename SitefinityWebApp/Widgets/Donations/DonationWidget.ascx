<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonationWidget.ascx.cs" Inherits="SitefinityWebApp.Widgets.Donations.DonationWidget" %>

<input type="hidden" name="cmd" value="_donations">
<input type="hidden" name="business" value="<%= PaypalEmail %>">
<input type="hidden" name="lc" value="US">
<input type="hidden" name="item_name" value="<%= OrganizationName %>">
<input type="hidden" name="no_note" value="0">
<input type="hidden" name="currency_code" value="USD">
<input type="hidden" name="bn" value="PP-DonationsBF:btn_donateCC_LG.gif:NonHostedGuest">
<asp:ImageButton ID="btnSubmit" runat="server" PostBackUrl="https://www.paypal.com/cgi-bin/webscr" AlternateText="PayPal - The safer, easier way to pay online!" />