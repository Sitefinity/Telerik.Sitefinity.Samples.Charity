<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Comments" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadListView ID="eventsList" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <ul class="sfeventsListSidebar sfeventsListTitleCityDate">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </ul>
    </LayoutTemplate>
    <ItemTemplate>
        <li class="sfeventListItem">
                <div class="sfeventTitle"><sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" TextDataField="Title" ToolTipDataField="Description" runat="server" /></div>
                <div class="sfeventDate"><sf:FieldListView ID="EventsDates" runat="server" /></div>
        </li>
    </ItemTemplate>
</telerik:RadListView>
<sf:Pager id="pager" runat="server" NavigationMode="Links"></sf:Pager>