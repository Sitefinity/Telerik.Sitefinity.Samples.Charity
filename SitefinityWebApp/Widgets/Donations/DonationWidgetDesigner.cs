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

        private string layoutTemplatePath = "~/Widgets/Donations/DonationWidgetDesignerTemplate.ascx";

        public override string LayoutTemplatePath
        {
            get { return this.layoutTemplatePath; }
            set { this.layoutTemplatePath = value; }
        }

        private string scriptPath = "~/Widgets/Donations/DonationWidgetDesigner.js";

        public string DesignerScriptPath
        {
            get { return this.scriptPath; }
            set { this.scriptPath = value; }
        }

        protected override string LayoutTemplateName
        {
            get { return "Donation Widget Designer Template"; }
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = base.GetScriptReferences() as List<ScriptReference>;
            if (scripts == null) return base.GetScriptReferences();

            scripts.Add(new ScriptReference(this.DesignerScriptPath));
            return scripts.ToArray();
        }
    }
}