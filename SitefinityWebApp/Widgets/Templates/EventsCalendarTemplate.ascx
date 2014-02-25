<%@ Control Language="C#" %>



<%@ Register Assembly="SitefinityWebApp" Namespace="SitefinityWebApp.Widgets.Calendar" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.Comments" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
 
<uc1:CalendarView ID="CalendarWidget1" runat="server" Width="100%" Height="400px" />
 
<!-- These are hidden because they are required by the template -->
<telerik:RadListView ID="eventsList" ItemPlaceholderID="ItemsContainer" runat="server" visible="false" />
<sf:Pager id="pager" runat="server" NavigationMode="Links" visible="false" />