<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.Comments" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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