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

        public static void FixRadioCheckbox(Page p)
        {
            // bug correction with radiobutton and checkbox because we need to wrap input inside label
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-fixRadioCheckbox"))
            {
                string script = @"fixRadioCheckbox();
                                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(fixRadioCheckbox);";

                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-fixRadioCheckbox", script, true);
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
                string initDate = @"$(initDatePicker);
                                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initDatePicker);";
                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-date", initDate, true);
            }
        }

        public static void InitFrmvldGovemail(Page p, string errorMsg)
        {
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-govemail"))
            {
                string patternGovEmail = @"$(document).on('wb-ready.wb', function (event) {{
                                                // make sure the validation is loaded
                                                frmvldGovemail('{0}');
                                            }});";
                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-govemail", string.Format(patternGovEmail, errorMsg), true);
            }
        }

        public static void InitFrmvldPrice(Page p, string errorMsg)
        {
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-price"))
            {
                string patternPrice = @"$(document).on('wb-ready.wb', function (event) {{
                                                // make sure the validation is loaded
                                                frmvldPrice('{0}');
                                            }});";
                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-price", string.Format(patternPrice, errorMsg), true);
            }
        }
    }
}
