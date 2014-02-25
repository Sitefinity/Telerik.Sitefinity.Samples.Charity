<%@ Control Language="C#" %>




<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks ID="ResourceLinks1" runat="server">
	<sitefinity:ResourceFile Name="~/Widgets/Calendar/CalendarViewTemplate.css" Static="True" />
</sitefinity:ResourceLinks>

<telerik:RadAjaxManagerProxy ID="ScriptMgr" runat="server">
	<AjaxSettings>
		<telerik:AjaxSetting AjaxControlID="EventCalendar">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="EventCalendar" LoadingPanelID="LoadingPanel" />
			</UpdatedControls>
		</telerik:AjaxSetting>
	</AjaxSettings>
</telerik:RadAjaxManagerProxy>

<telerik:RadCalendar ID="EventCalendar" runat="server" DayNameFormat="Short" EnableMultiSelect="False" PresentationType="Preview" 
	SelectedDate="" ShowRowHeaders="False" CssClass="EventCalendar"	ViewSelectorText="x" AutoPostBack="True" />

<telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" EnableSkinTransparency="true"	Transparency="50" BackgroundPosition="Center" />
