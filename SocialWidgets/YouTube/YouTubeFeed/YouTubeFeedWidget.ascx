<%@ Control Language="C#" %>




<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks runat="server" UseEmbeddedThemes="True">
	<sitefinity:ResourceFile AssemblyInfo="Sitefinity.Widgets.Social.YouTube.YouTubeFeedWidget, Sitefinity.Widgets.Social" Name="Sitefinity.Widgets.Social.YouTube.YouTubeFeed.YouTubeFeedWidget.css"
		Static="true" />
</sitefinity:ResourceLinks>

<div class="youtubefeed">
	<div class="youtubeheader">
		<asp:HyperLink ID="HeaderLink" runat="server" Target="_blank" />
		on Youtube
	</div>
	<ul>
		<asp:Repeater ID="YouTubeRepeater" runat="server">
			<ItemTemplate>
				<li><a href="<%# Eval("Url") %>" title="<%# Eval("Title") %>" target="_blank">
					<img src="<%# Eval("Thumbnail") %>" alt="<%# Eval("Title") %>" />
				</a>
					<asp:HyperLink ID="lnkTitle" runat="server" Target="_blank" />
					<div style="clear: both">
					</div>
				</li>
			</ItemTemplate>
		</asp:Repeater>
	</ul>
	<div class="youtubefooter">
		<asp:HyperLink ID="FooterLink" runat="server" Target="_blank">More Videos on YouTube...</asp:HyperLink>
	</div>
</div>