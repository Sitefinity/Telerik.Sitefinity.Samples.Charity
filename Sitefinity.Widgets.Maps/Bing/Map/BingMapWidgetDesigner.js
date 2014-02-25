Type.registerNamespace("Sitefinity.Widgets.Maps.Bing");

Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner = function (element) {
	Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner.initializeBase(this, [element]);
}


Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner.SearchMap = function () {
	jQuery("#LoadingPanel").css("display", "block");

	// get address properties
	var street = jQuery.trim($("#Street").val());
	var city = jQuery.trim($("#City").val());
	var state = jQuery.trim($("#State").val());
	var zipcode = jQuery.trim($("#Zipcode").val());

	// validate address
	var address = street + ' ' + city + ' ' + state + ' ' + zipcode;
	if (address.length < 1 || city.length < 1 || state.length < 1) {
		jQuery("#LoadingPanel").css("display", "none");
		alert("Address cannot be blank");
		return;
	}

	// geocode address using map
	map = new VEMap('hiddenmap');
	map.LoadMap();
	map.Find("", address, null, null, 0, 1, true, true, true, true, Sitefinity.Widgets.Maps.Bing.FindCallBack);
}

Sitefinity.Widgets.Maps.Bing.FindCallBack = function(layer, resultsArray, places, hasMore, VEErrorMessage) {

	// any places found?
	if (places == null) {
		jQuery("#LoadingPanel").css("display", "none");
		alert("Address not found, please try again.");
		return;
	}

	// bind property to found address
	$.each(places, function (i, item) {
		$("#Latitude").val(item.LatLong.Latitude.toString());
		$("#Longitude").val(item.LatLong.Longitude.toString());
	});
	jQuery("#LoadingPanel").css("display", "none");
}

Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner.prototype = {
	initialize: function () {
		Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner.callBaseMethod(this, 'initialize');
		jQuery("#Geocode").click(Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner.SearchMap);
	},
	dispose: function () {
		Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner.callBaseMethod(this, 'dispose');
	},
	refreshUI: function () {
		var data = this._propertyEditor.get_control();

		// bind widget properites to designer
		jQuery("#Location").val(data.Location);
		jQuery("#Street").val(data.Street);
		jQuery("#City").val(data.City);
		jQuery("#State").val(data.State);
		jQuery("#Zipcode").val(data.Zipcode);

		jQuery("#Width").val(data.Width);
		jQuery("#Height").val(data.Height);
		jQuery("#Zoom").val(data.Zoom);
		jQuery("#DashboardSize").val(data.DashboardSize);

		jQuery("#Latitude").val(data.Latitude);
		jQuery("#Longitude").val(data.Longitude);


	},
	applyChanges: function () {

		var controlData = this._propertyEditor.get_control();

		// bind designer properties back to widget
		controlData.Location = jQuery("#Location").val();
		controlData.Street = jQuery("#Street").val();
		controlData.City = jQuery("#City").val();
		controlData.State = jQuery("#State").val();
		controlData.Zipcode = jQuery("#Zipcode").val();

		controlData.Width = jQuery("#Width").val();
		controlData.Height = jQuery("#Height").val();
		controlData.Zoom = jQuery("#Zoom").val();
		controlData.DashboardSize = jQuery("#DashboardSize").val();

		controlData.Latitude = jQuery("#Latitude").val();
		controlData.Longitude = jQuery("#Longitude").val();

	}
}

Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner.registerClass('Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();