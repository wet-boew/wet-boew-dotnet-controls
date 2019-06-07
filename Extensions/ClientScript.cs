using System.Web.UI;

[assembly: System.Web.UI.WebResource("WetControls.Scripts.wetscripts.js", "text/javascript")]
namespace WetControls.Extensions
{
    public static class ClientScript
    {
        public static void InitScript(Page p)
        {
            // load init scripts
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-init"))
            {
                // load custom scripts
                System.Web.UI.ScriptManager sm = System.Web.UI.ScriptManager.GetCurrent(p);
                if (sm == null) throw new System.Exception("You must have a script manager in your page.");

                sm.Scripts.Add(new System.Web.UI.ScriptReference("WetControls.Scripts.wetscripts.js", "WetControls"));

                string script = @"wrapForm();
                                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(wrapForm);
                                Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(onEachRequest);";

                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-init", script, true);
            }
        }

        public static void FixCheckBoxList(Page p)
        {
            // bug correction with checkboxlist because we can not put the same name for required validation in web form
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-checkboxlist"))
            {
                string script = @"fixCheckBoxList();
                                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(fixCheckBoxList);";

                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-checkboxlist", script, true);
            }
        }

        public static void ValidateScript(Page p)
        {
            string script = @"if ($.validator && $.validator !== 'undefined') {
                                $(postbackValidation);
                            } else {
                                $(document).on('wb-ready.wb', function(event) {
                                    postbackValidation();
                                });
                            }";

            ScriptManager.RegisterStartupScript(p, p.GetType(), "wb-frmvld-validate", script, true);
        }

        public static void InitDatePicker(Page p)
        {
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-date"))
            {
                // postack event initialisation
                string initDate = @"Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initDatePicker);";
                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-date", initDate, true);
            }
        }
    }
}
