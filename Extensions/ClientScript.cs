using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace WetControls.Extensions
{
    public static class ClientScript
    {
        public static void InitScript(Page p)
        {
            // wrap form with validation class and cancel submit if not valid
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-init"))
            {
                string script = @"if ($('.wb-frmvld').length === 0) {
                                    // wrap all the form with the class for the validation
                                    $('body').wrapInner('<div class=""wb-frmvld""></div>');
                                }

                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                prm.add_initializeRequest(onEachRequest);

                                function onEachRequest(sender, args) {
                                    var element = args.get_postBackElement();
                                    if (element.type === 'submit' && !element.hasAttribute('formnovalidate')) {
                                        args.set_cancel(!$('form').valid());
                                    }
                                };";

                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-init", script, true);
            }
        }

        public static void FixCheckBoxList(Page p)
        {
            // bug correction with checkboxlist because we can not put the same name for required validation in web form
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-checkboxlist"))
            {
                string script = @"fixCheckBoxList();
                                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(fixCheckBoxList);
                                function fixCheckBoxList() {
                                    $(':checkbox[data-rule-require_from_group]').closest('.checkbox').siblings('div').on('change', function (evt) {
                                        var input = $(this).closest('fieldset').find(':checkbox[data-rule-require_from_group]');
                                        $(this).closest('form').data('validator').element(input);
                                    });
                                };";

                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-checkboxlist", script, true);
            }
        }

        public static void SummaryScript(Page p, bool displaySummary, string clientID)
        {
            // catch the event to position the summary
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-summary"))
            {
                string action = displaySummary ? "$(e.target).appendTo('#" + clientID + "');" : "$(e.target).hide();";
                string script = @"$(document).on('DOMNodeInserted', function (e) { if ($(e.target).is('[id^=errors-]')) { " + action + "} });";
                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-summary", script, true);
            }
        }

        public static void ValidateScript(Page p, string clientID)
        {
            // validate a unique control after async postback
            string script = "$(function() { $('#" + clientID + "').valid(); });";
            ScriptManager.RegisterClientScriptBlock(p, typeof(string), "wb-frmvld-validate-" + clientID, script, true);
        }
    }
}
