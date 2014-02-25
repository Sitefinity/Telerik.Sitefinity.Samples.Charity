Type.registerNamespace("Sitefinity.Widgets.Social.Flickr");

Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner = function (element) {
    Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner.initializeBase(this, [element]);
}


Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner.prototype = {
    initialize: function () {
        Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner.callBaseMethod(this, 'initialize');

    },
    dispose: function () {
        Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner.callBaseMethod(this, 'dispose');
    },
    refreshUI: function () {
        var data = this._propertyEditor.get_control();
        jQuery("#UserID").val(data.UserID);
        jQuery("#Username").val(data.Username);
        jQuery("#MaxPhotos").val(data.MaxPhotos);
        jQuery("#ShowTitles").attr('checked', data.ShowTitles);
    },
    applyChanges: function () {

        var controlData = this._propertyEditor.get_control();

        controlData.UserID = jQuery("#UserID").val();
        controlData.Username = jQuery("#Username").val();
        controlData.MaxPhotos = jQuery("#MaxPhotos").val();
        controlData.ShowTitles = jQuery("#ShowTitles").attr('checked');
    }
}

Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner.registerClass('Sitefinity.Widgets.Social.Flickr.FlickrFeedWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();