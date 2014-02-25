Type.registerNamespace("Sitefinity.Widgets.Social.Facebook");

Sitefinity.Widgets.Social.Facebook.LikeButtonWidgetDesigner = function (element) {
	Sitefinity.Widgets.Social.Facebook.LikeButtonWidgetDesigner.initializeBase(this, [element]);
}

Sitefinity.Widgets.Social.Facebook.LikeButtonWidgetDesigner.prototype = {
	initialize: function () {
		Sitefinity.Widgets.Social.Facebook.LikeButtonWidgetDesigner.callBaseMethod(this, 'initialize');

		// resize based on layout type
		jQuery("#Layout").change(function () {
			var layout = jQuery(this).val();
			var w, h;
			switch (layout) {
				case "Standard":
					w = 450;
					h = jQuery("#ShowFaces").attr('checked') ? 80 : 35;
					break;

				case "Button_Count":
					w = 90;
					h = 20;
					break;

				case "Box_Count":
					w = 55;
					h = 65;
					break;
			}

			jQuery("#WidgetWidth").val(w);
			jQuery("#WidgetHeight").val(h);
		});

		// resize on faces checkbox
		jQuery("#ShowFaces").click(function () {
			if (jQuery(this).is(':checked') && jQuery("#Layout").val() == "Standard")
				jQuery("#WidgetHeight").val(80);
			else
				jQuery("#WidgetHeight").val(35);
		});

	},
	dispose: function () {
		Sitefinity.Widgets.Social.Facebook.LikeButtonWidgetDesigner.callBaseMethod(this, 'dispose');
	},
	refreshUI: function () {
		var data = this._propertyEditor.get_control();
		jQuery("#WidgetMode").val(data.WidgetMode);
		jQuery("#Action").val(data.Action);
		jQuery("#Layout").val(data.Layout);

		jQuery("#WidgetWidth").val(data.WidgetWidth);
		jQuery("#WidgetHeight").val(data.WidgetHeight);
		jQuery("#NumConnections").val(data.NumConnections);
		jQuery("#ColorScheme").val(data.ColorScheme);
		jQuery("#ShowFaces").attr('checked', data.ShowFaces);
		jQuery("#Font").val(data.Font);

	},
	applyChanges: function () {

		var controlData = this._propertyEditor.get_control();

		controlData.WidgetMode = jQuery("#WidgetMode").val();
		controlData.Action = jQuery("#Action").val();
		controlData.Layout = jQuery("#Layout").val();
		controlData.WidgetWidth = jQuery("#WidgetWidth").val();
		controlData.WidgetHeight = jQuery("#WidgetHeight").val();
		controlData.NumConnections = jQuery("#NumConnections").val();
		controlData.ColorScheme = jQuery("#ColorScheme").val();
		controlData.ShowFaces = jQuery("#ShowFaces").attr('checked');
		controlData.Font = jQuery("#Font").val();
	}
}

Sitefinity.Widgets.Social.Facebook.LikeButtonWidgetDesigner.registerClass('Sitefinity.Widgets.Social.Facebook.LikeButtonWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();