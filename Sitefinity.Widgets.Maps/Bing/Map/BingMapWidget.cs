using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Maps.Bing
{
	/// <summary>
	/// Sitefinity Widget to display a Bing Map for a given address
	/// </summary>
	[ControlDesigner(typeof(Sitefinity.Widgets.Maps.Bing.BingMapWidgetDesigner)), PropertyEditorTitle("Bing Map")]
    [RequireScriptManager]
	public partial class BingMapWidget : SimpleView
	{

		#region Private Properties

		private string _location = "Telerik";
		private string _street = "10200 Grogans Mill Rd, Suite 130";
		private string _city = "The Woodlands";
		private string _state = "TX";
		private string _zipcode = "77380";

		private string _latitude = "30.15667";
		private string _longitude = "-95.471005";
		private string _zoom = "14";

		private Unit _width = new Unit("400px");
		private Unit _height = new Unit("300px");
		private string _dashboardSize = "Normal";

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the name of the location (company, building, etc).
		/// </summary>
		/// <value>
		/// The location name.
		/// </value>
		public string Location
		{
			get { return _location; }
			set { _location = value; }
		}

		/// <summary>
		/// Gets or sets the street address.
		/// </summary>
		/// <value>
		/// The physical street address.
		/// </value>
		public string Street
		{
			get { return _street; }
			set { _street = value; }
		}

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>
		/// The city.
		/// </value>
		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public string State
		{
			get { return _state; }
			set { _state = value; }
		}

		/// <summary>
		/// Gets or sets the zipcode.
		/// </summary>
		/// <value>
		/// The zipcode.
		/// </value>
		public string Zipcode
		{
			get { return _zipcode; }
			set { _zipcode = value; }
		}

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>
		/// The latitude.
		/// </value>
		public string Latitude
		{
			get { return _latitude; }
			set { _latitude = value; }
		}

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>
		/// The longitude.
		/// </value>
		public string Longitude
		{
			get { return _longitude; }
			set { _longitude = value; }
		}

		/// <summary>
		/// Gets or sets the zoom level of the map.
		/// </summary>
		/// <value>
		/// The map zoom level.
		/// </value>
		public string Zoom
		{
			get { return _zoom; }
			set { _zoom = value; }
		}

		/// <summary>
		/// Gets or sets the width of the map.
		/// </summary>
		/// <value>
		/// The map width (in pixels).
		/// </value>
		public override Unit Width
		{
			get { return _width; }
			set { _width = value; }
		}

		/// <summary>
		/// Gets or sets the height of the map.
		/// </summary>
		/// <value>
		/// The map height (in pixels).
		/// </value>
		public override Unit Height
		{
			get { return _height; }
			set { _height = value; }
		}

		/// <summary>
		/// Gets or sets the size of the map dashboard.
		/// </summary>
		/// <value>
		/// The size of the map dashboard.
		/// </value>
		public string DashboardSize
		{
			get { return _dashboardSize; }
			set { _dashboardSize = value; }
		}

		#endregion

		#region Constants

		/// <summary>
		/// Format string for the map javascript code.
		/// </summary>
		const string MAP_JAVASCRIPT = @"<script type=""text/javascript"">
		function GetMap_NAMING_ID() {
		map = new VEMap('NAMING_ID');
		map.SetDashboardSize(VEDashboardSize.DASHBOARD_SIZE);
		map.LoadMap(new VELatLong(POINT_LATITUDE, POINT_LONGITUDE), ZOOM_LEVEL);
		
		var shape;

		shape = new VEShape(VEShapeType.Pushpin, new VELatLong(POINT_LATITUDE, POINT_LONGITUDE));
		shape.SetTitle(""POINT_TITLE"");
		shape.SetDescription('<p><strong>POINT_ADDRESS</strong><br />POINT_CITY, POINT_STATE POINT_ZIPCODE</p>');
		map.AddShape(shape);
		}

		$(function() { GetMap_NAMING_ID();});
		</script>";

		#endregion

		#region Template Controls

		/// <summary>
		/// The Repeater control to render tweets on the page.
		/// </summary>
		protected virtual Panel BingMapPanel
		{
			get { return this.Container.GetControl<Panel>("BingMapPanel", true); }
		}

		/// <summary>
		/// Gets the name of the layout template.
		/// </summary>
		/// <value>
		/// The name of the layout template.
		/// </value>
		protected override string LayoutTemplateName
		{
			get { return null; }
		}

        /// <summary>
        /// Gets or sets the path of the external template to be used by the control.
        /// </summary>
        /// <value></value>
        public override string LayoutTemplatePath
        {
            get
            {
                return Resources.VirtualPathPrefix + "Sitefinity.Widgets.Maps.Bing.Map.BingMapWidget.ascx";
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.</returns>
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
		protected override void InitializeControls(GenericContainer container)
		{
			
		}

		#endregion

		/// <summary>
		/// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			EnsureChildControls();

			// initialize map script with user properties
			var script = MAP_JAVASCRIPT.Replace("NAMING_ID", BingMapPanel.ClientID);
			script = script.Replace("DASHBOARD_SIZE", DashboardSize); 
			script = script.Replace("POINT_LATITUDE", Latitude);
			script = script.Replace("POINT_LONGITUDE", Longitude);
			script = script.Replace("ZOOM_LEVEL", Zoom);
			script = script.Replace("POINT_TITLE", Location);
			script = script.Replace("POINT_ADDRESS", Street);
			script = script.Replace("POINT_CITY", City);
			script = script.Replace("POINT_STATE", State);
			script = script.Replace("POINT_ZIPCODE", Zipcode);

			// only register global bing map script once
			if (!Page.IsClientScriptBlockRegistered("Bing.Map.Script"))
				Page.ClientScript.RegisterClientScriptInclude("Bing.Map.Script", "http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2");

			// render this widgets map script
			Page.ClientScript.RegisterStartupScript(typeof(BingMapWidget), BingMapPanel.ClientID, script, false);

			// set properties
			BingMapPanel.Width = Width;
			BingMapPanel.Height = Height;
		}
	}
}