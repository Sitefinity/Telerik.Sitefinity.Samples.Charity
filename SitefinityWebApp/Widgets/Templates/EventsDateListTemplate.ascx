<%@ Control Language="C#" %>



<%@ Register Assembly="SitefinityWebApp" Namespace="SitefinityWebApp.Tools" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadListView ID="eventsList" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <h1>Events</h1>
        <ul class="sfeventsList sfeventsListTitleCityDateContent">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </ul>
    </LayoutTemplate>
    <ItemTemplate>
        <li class="sfeventListItem">
            <div class="sfeventDateSummary">
               <div class="sfeventDay"><%# Eval("EventStart", "{0:ddd}")%></div>
               <div class="sfeventDate"><%# Eval("EventStart", "{0:dd}")%></div>
               <div class="sfeventMonth"><%# Eval("EventStart", "{0:MMM}")%></div>
            </div>

            <h3 class="sfeventTitle">
                <sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" TextDataField="Title" ToolTipDataField="Description" runat="server" />
            </h3>
            <div class="sfeventMetaInfo">
                <sf:FieldListView ID="where" runat="server" Text="{0} | " Properties="City"   /> 
                <sf:FieldListView ID="EventsDates" runat="server" />
            </div>
            <div class="sfeventContent">
                <cc1:Truncate Text='<%# Eval("Content")%>' Length="200" runat="server" />
                <sf:DetailsViewHyperLink ID="DetailsViewHyperLink2" Text="Read more" runat="server" />
            </div>
        </li>
    </ItemTemplate>
</telerik:RadListView>
<sf:Pager id="pager" runat="server" NavigationMode="Links"></sf:Pager>