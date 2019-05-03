using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // add startup init script
            WetControls.Extensions.ClientScript.InitScript(Page);

            // catch summary inserted
            WetControls.Extensions.ClientScript.SummaryScript(this.Page, DisplaySummary, this.ClientID);
        }
    }
}
