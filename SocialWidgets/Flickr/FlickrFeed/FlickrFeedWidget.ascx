<%@ Control Language="C#" %>




<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks runat="server" UseEmbeddedThemes="True">
	<sitefinity:ResourceFile AssemblyInfo="Sitefinity.Widgets.Social.Flickr.FlickrFeedWidget, Sitefinity.Widgets.Social" Name="Sitefinity.Widgets.Social.Flickr.FlickrFeed.FlickrFeedWidget.css" Static="true" />
</sitefinity:ResourceLinks>
<div class="flickrfeed">
	<div class="flickrheader">
		<asp:HyperLink ID="HeaderLink" runat="server" Target="_blank" />
		on Flickr
	</div>
	<ul>
		<asp:Repeater ID="FlickrRepeater" runat="server">
			<ItemTemplate>
				<li><a href="<%# Eval("Url") %>" target="_blank" title="<%# Eval("Title") %>">
					<img src="<%# Eval("Thumbnail") %>" alt="<%# Eval("Title") %>" /></a>
					<asp:HyperLink ID="lnkTitle" runat="server" Target="_blank" />
				</li>
			</ItemTemplate>
		</asp:Repeater>
	</ul>
	<div style="clear: both;">
	</div>
	<div class="flickrfooter">
		<asp:HyperLink ID="FooterLink" runat="server" Target="_blank" Text="More Photos on Flickr..." />
	</div>
</div>