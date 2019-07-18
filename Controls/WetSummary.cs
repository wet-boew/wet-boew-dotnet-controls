using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: System.Web.UI.WebResource("WetControls.Scripts.ready.js", "text/javascript")]
namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetSummary runat=\"server\"></{0}:WetSummary>")]
    public class WetSummary : Panel
    {
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        ]
        public bool DisplaySummary
        {
            get
            {
                object o = ViewState["DisplaySummary"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["DisplaySummary"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            // startup init script
            WetControls.Extensions.ClientScript.InitScript(Page);

            // catch summary inserted
            this.SummaryScript();

            base.OnLoad(e);
        }

        private void SummaryScript()
        {
            // catch the event to position the summary
            if (!this.Page.ClientScript.IsStartupScriptRegistered("wb-frmvld-summary"))
            {
                // load script
                System.Web.UI.ScriptManager sm = System.Web.UI.ScriptManager.GetCurrent(this.Page);
                if (sm == null) throw new Exception("You must have a script manager in your page.");

                sm.Scripts.Add(new System.Web.UI.ScriptReference("WetControls.Scripts.ready.js", "WetControls"));

                string action = this.DisplaySummary ? "$(element).appendTo('#" + this.ClientID + "');" : "$(element).hide();";
                string script = @"// Detect element availability on the initial page load and those dynamically appended to the DOM
                                  var formId = $('form').attr('id');
                                  var errorFormId = '#errors-' + (!formId ? 'default' : formId);
                                  ready(errorFormId, function(element) { " + action + "});";

                this.Page.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-summary", script, true);
            }
        }
    }
}
