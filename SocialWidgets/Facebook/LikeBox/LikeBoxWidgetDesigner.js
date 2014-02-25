Type.registerNamespace("Sitefinity.Widgets.Social.Facebook");

Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner = function (element) {
    Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner.initializeBase(this, [element]);
}


Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner.prototype = {
    initialize: function () {
        Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner.callBaseMethod(this, 'initialize');

    },
    dispose: function () {
        Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner.callBaseMethod(this, 'dispose');
    },
    refreshUI: function () {
        var data = this._propertyEditor.get_control();
        jQuery("#WidgetMode").val(data.WidgetMode);
        jQuery("#Url").val(data.Url);
        jQuery("#WidgetWidth").val(data.WidgetWidth);
        jQuery("#WidgetHeight").val(data.WidgetHeight);
        jQuery("#NumConnections").val(data.NumConnections);
        jQuery("#ColorScheme").val(data.ColorScheme);
        jQuery("#ShowHeader").attr('checked', data.ShowHeader);
        jQuery("#ShowStream").attr('checked', data.ShowStream);

    },
    applyChanges: function () {

        var controlData = this._propertyEditor.get_control();

        controlData.WidgetMode = jQuery("#WidgetMode").val();
        controlData.Url = jQuery("#Url").val();
        controlData.WidgetWidth = jQuery("#WidgetWidth").val();
        controlData.WidgetHeight = jQuery("#WidgetHeight").val();
        controlData.NumConnections = jQuery("#NumConnections").val();
        controlData.ColorScheme = jQuery("#ColorScheme").val();
        controlData.ShowHeader = jQuery("#ShowHeader").attr('checked');
        controlData.ShowStream = jQuery("#ShowStream").attr('checked');
    }
}

Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner.registerClass('Sitefinity.Widgets.Social.Facebook.LikeBoxWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();