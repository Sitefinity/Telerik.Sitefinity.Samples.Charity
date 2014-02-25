<%@ Control Language="C#" %>




<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks runat="server" UseEmbeddedThemes="True">
	<sitefinity:ResourceFile AssemblyInfo="Sitefinity.Widgets.Social.Twitter.TwitterFeedWidget, Sitefinity.Widgets.Social" Name="Sitefinity.Widgets.Social.Twitter.TwitterFeed.TwitterFeedWidget.css" Static="true" />
</sitefinity:ResourceLinks>

<div class="twitterfeed">
	<div class="twitterheader">
		<asp:Image ID="TwitterProfileIcon" runat="server" />
		<asp:HyperLink ID="HeaderLink" runat="server" Target="_blank" />
		on Twitter
	</div>
	<ul>
		<asp:Repeater ID="TwitterRepeater" runat="server">
			<ItemTemplate>
				<li class="twittertweet">
					<p><%# Eval("Tweet") %></p>
					<p class="reply">
						<asp:HyperLink ID="lnkTimeStamp" runat="server" Target="_blank" /> 
						<asp:Literal ID="Separator" runat="server" Text=" &bull; " Visible="false" />
						<asp:HyperLink ID="lnkReply"  runat="server" Target="_blank" Text="Reply" />
					</p>
				</li>
			</ItemTemplate>
		</asp:Repeater>
		
		<asp:Literal ID="ErrorView" runat="server" Text="<li>Twitter feed unavailable. Please try again.</li>" Visible="false" />
	</ul>
	<div class="twitterfooter">
		<a href="http://twitter.com" target="_blank"><asp:Image ID="TwitterLogo" runat="server" AlternateText="Twitter Logo" /></a>
		<asp:HyperLink ID="FooterLink" runat="server" Target="_blank">Join the Conversation</asp:HyperLink>
	</div>
</div>