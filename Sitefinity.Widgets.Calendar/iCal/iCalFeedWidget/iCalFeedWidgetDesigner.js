Type.registerNamespace("Sitefinity.Widgets.Calendar.iCalFeed");

Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner = function (element) {
	Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner.initializeBase(this, [element]);
}

Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner.prototype = {
	initialize: function () {
		Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner.callBaseMethod(this, 'initialize');
	},
	dispose: function () {
		Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner.callBaseMethod(this, 'dispose');
	},
	refreshUI: function () {
		var data = this._propertyEditor.get_control();

		// bind widget properites to designer
		jQuery("#FeedUrl").val(data.FeedUrl);


	},
	applyChanges: function () {

		var controlData = this._propertyEditor.get_control();

		// bind designer properties back to widget
		controlData.FeedUrl = jQuery("#FeedUrl").val();

	}
}

Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner.registerClass('Sitefinity.Widgets.Calendar.iCalFeed.iCalFeedWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();