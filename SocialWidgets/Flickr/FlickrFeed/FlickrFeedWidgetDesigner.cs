using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Sitefinity.Widgets.Social.Flickr
{
	public class FlickrFeedWidgetDesigner : ControlDesignerBase
	{
		// embedded resources
		private const string LayoutTemplateReference = "Sitefinity.Widgets.Social.Flickr.FlickrFeed.FlickrFeedWidgetDesignerTemplate.ascx";
		private const string DesignerScriptReference = "Sitefinity.Widgets.Social.Flickr.FlickrFeed.FlickrFeedWidgetDesigner.js";

		/// <summary>
		/// Initializes the controls.
		/// </summary>
		/// <param name="container">The container.</param>
		protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
		{
			base.DesignerMode = ControlDesignerModes.Simple;
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
                return Resources.VirtualPathPrefix + LayoutTemplateReference;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
		/// <summary>
		/// Gets the script references.
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			// get script collection
			var scripts = base.GetScriptReferences() as List<ScriptReference>;
			if (scripts == null) return base.GetScriptReferences();

			// add script from embedded resource
			var assemblyName = this.GetType().Assembly.GetName().ToString();
			scripts.Add(new ScriptReference(DesignerScriptReference, assemblyName));
			return scripts.ToArray();
		}
	}
}