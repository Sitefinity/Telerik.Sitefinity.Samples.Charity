Type.registerNamespace("SitefinityWebApp.Widgets.Donations");

SitefinityWebApp.Widgets.Donations.DonationWidgetDesigner = function (element) {
    SitefinityWebApp.Widgets.Donations.DonationWidgetDesigner.initializeBase(this, [element]);
}


SitefinityWebApp.Widgets.Donations.DonationWidgetDesigner.prototype = {
    initialize: function () {
        SitefinityWebApp.Widgets.Donations.DonationWidgetDesigner.callBaseMethod(this, 'initialize');

    },
    dispose: function () {
        SitefinityWebApp.Widgets.Donations.DonationWidgetDesigner.callBaseMethod(this, 'dispose');
    },
    refreshUI: function () {
        var data = this._propertyEditor.get_control();
        jQuery("#PaypalEmail").val(data.PaypalEmail);
        jQuery("#OrganizationName").val(data.OrganizationName);
        jQuery("#ShowCreditCardLogos").attr('checked', data.ShowCreditCardLogos);
    },
    applyChanges: function () {

        var controlData = this._propertyEditor.get_control();

        controlData.PaypalEmail = jQuery("#PaypalEmail").val();
        controlData.OrganizationName = jQuery("#OrganizationName").val();
        controlData.ShowCreditCardLogos = jQuery("#ShowCreditCardLogos").attr('checked');
    }
}

SitefinityWebApp.Widgets.Donations.DonationWidgetDesigner.registerClass('SitefinityWebApp.Widgets.Donations.DonationWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();