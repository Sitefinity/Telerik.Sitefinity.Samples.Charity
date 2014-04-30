<%@ Control Language="C#" %>
<%@ Import Namespace="Telerik.Sitefinity" %>	



<%@ Register Assembly="SitefinityWebApp" Namespace="SitefinityWebApp.Tools" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Comments" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<h1>Charity Blog</h1>
<telerik:RadListView ID="Repeater" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <ul class="sfpostsList sfpostListTitleDateSummary">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </ul>
    </LayoutTemplate>
    <ItemTemplate>
        <li class="sfpostListItem">
            
            <h3 class="sfpostTitle">
                <sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" TextDataField="Title" ToolTipDataField="Description" runat="server" />
            </h3>
            
            <div class="sfpostAuthorAndDate">
                <asp:Literal ID="Literal2" Text="<%$ Resources:Labels, By %>" runat="server" /> 
                <sf:PersonProfileView ID="PersonProfileView1" runat="server" />
                <sf:FieldListView ID="PostDate" runat="server" Format=" | {PublicationDate.ToLocal():MMM dd, yyyy}" />
            </div>

            <cc1:Truncate ID="Truncate1" Text='<%# Eval("Content")%>' Length="200" runat="server" />
            <sf:DetailsViewHyperLink ID="FullStory" Text="Full story" runat="server" CssClass="sfpostFullStory" />

        </li>
    </ItemTemplate>
</telerik:RadListView>
<sf:Pager id="pager" runat="server" NavigationMode="Links"></sf:Pager>