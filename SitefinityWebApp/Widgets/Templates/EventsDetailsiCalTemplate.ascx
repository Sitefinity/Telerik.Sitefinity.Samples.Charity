<%@ Control Language="C#" %>
<%@ Import Namespace="Telerik.Sitefinity" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>	

<telerik:RadListView ID="SingleItemContainer" ItemPlaceholderID="ItemContainer" AllowPaging="False" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <div class="sfeventDetails">
            <asp:PlaceHolder ID="ItemContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <h1 class="sfeventTitle">
            <asp:Literal ID="Literal1" Text='<%# Eval("Title") %>' runat="server" />
        </h1>
<p class="sfeventLinksWrp">
<a class="outlook" href="<%= ResolveUrl("~/ical/event/") %><%# Eval("Id") %>">Add Reminder</a>
</p>
        <ul class="sfeventDatesLocationContacts">
            <sf:FieldListView ID="EventDates" runat="server" WrapperTagName="li" />
            <sf:FieldListView ID="Location" runat="server" 
                Text="<%$ Resources:EventsResources, Where %> {0}" Properties="City, State, Country" 
                WrapperTagName="li"
            />
            <sf:FieldListView ID="Street" runat="server" 
                Text="<%$ Resources:EventsResources, Address %> {0}" Properties="Street" 
                WrapperTagName="li"
            />
            <sf:FieldListView ID="ContactName" runat="server" 
                Text="<%$ Resources:EventsResources, ContactName %> {0}" Properties="ContactName, ContactEmail" 
                WrapperTagName="li"
            />
            <sf:FieldListView ID="Web" runat="server" 
                Text="<%$ Resources:EventsResources, WebSite %> {0}" Properties="ContactWeb" 
                WrapperTagName="li"
            />
            <sf:FieldListView ID="Phone" runat="server" 
                Text="<%$ Resources:EventsResources, ContactPhone %> {0}" Properties="ContactPhone, ContactCell" 
                WrapperTagName="li"
            />
        </ul>

        <div class="sfeventContent">
            <asp:Literal ID="Literal4" Text='<%# Eval("Content") %>' runat="server" />
        </div>

            <p class="sfeventLinksWrp">
                <sf:MasterViewHyperLink class="sfeventBack" Text="<%$ Resources:EventsResources, AllEvents %>" runat="server" />
            </p>

        <sf:ContentView 
             id="commentsListView" 
             ControlDefinitionName="EventsCommentsFrontend" 
             DetailViewName="CommentsMasterView"
             ContentViewDisplayMode="Master"
             runat="server" />
        <sf:ContentView 
             id="commentsDetailsView" 
             ControlDefinitionName="EventsCommentsFrontend" 
             DetailViewName="CommentsDetailsView"
             ContentViewDisplayMode="Detail"
             runat="server" />
    </ItemTemplate>
</telerik:RadListView>