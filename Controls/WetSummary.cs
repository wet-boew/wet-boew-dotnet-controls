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
        DefaultValue(""),
        ]
        public string AssociatedFormID
        {
            get
            {
                string t = (string)ViewState["AssociatedFormID"];
                return (t == null) ? String.Empty : t;
            }
            set { ViewState["AssociatedFormID"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        ]
        public bool DisplaySummary
        {
            get
            {
                object o = ViewState["IsNumber"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["IsNumber"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // wrap all the form with the class for the validation
            if (!Page.ClientScript.IsStartupScriptRegistered("wb-frmvld-wrap"))
            {
                string wrap = @"if ($('.wb-frmvld').length === 0)
                                    $('body').wrapInner('<div class=""wb-frmvld""></div>');";

                Page.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-wrap", wrap, true);
            }

            string script;
            if (DisplaySummary)
            {
                script = @"$(document).on('DOMNodeInserted', function (e) {{
                                if ($(e.target).is('{0}'))
                                    $(e.target).appendTo('#{1}');
                            }});";
            }
            else
            {
                script = @"$(document).on('DOMNodeInserted', function (e) {{
                                if ($(e.target).is('{0}'))
                                    $(e.target).hide();
                            }});";
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "wb-frmvld-script", string.Format(script, string.IsNullOrEmpty(AssociatedFormID) ? "[id^=errors-]" : "#errors-" + AssociatedFormID, this.ClientID), true);
        }
    }
}
