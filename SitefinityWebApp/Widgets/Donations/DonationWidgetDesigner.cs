using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace SitefinityWebApp.Widgets.Donations
{
    public class DonationWidgetDesigner : ControlDesignerBase
    {
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            base.DesignerMode = ControlDesignerModes.Simple;
        }

        private string _layoutTemplatePath = "~/Widgets/Donations/DonationWidgetDesignerTemplate.ascx";

        public override string LayoutTemplatePath
        {
            get { return _layoutTemplatePath; }
            set { _layoutTemplatePath = value; }
        }

        private string _scriptPath = "~/Widgets/Donations/DonationWidgetDesigner.js";

        public string DesignerScriptPath
        {
            get { return _scriptPath; }
            set { _scriptPath = value; }
        }

        protected override string LayoutTemplateName
        {
            get { return "Donation Widget Designer Template"; }
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = base.GetScriptReferences() as List<ScriptReference>;
            if (scripts == null) return base.GetScriptReferences();

            scripts.Add(new ScriptReference(DesignerScriptPath));
            return scripts.ToArray();
        }
    }
}