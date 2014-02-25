<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks ID="ResourceLinks1" runat="server">
	<sitefinity:ResourceFile Name="~/widgets/Calendar/iCalFeedWidget.css" Static="True" />
</sitefinity:ResourceLinks>

<div class="iCalFeed">
	<asp:HyperLink ID="FeedLink" runat="server" NavigateUrl="~/ical/feed" />
</div>
