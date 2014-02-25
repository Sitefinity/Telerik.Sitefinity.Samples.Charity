<%@ Control Language="C#" %>

<asp:Panel ID="BingMapPanel" runat="server" style="position: relative">
	<address>
		<p>Loading Map (Requres Javascript) . . .</p>
		<asp:Literal ID="PreviewModeText" runat="server" Text="<p>(Preview in Design Mode is currently unsupported)</p>"  Visible="false" />
	</address>
</asp:Panel>
