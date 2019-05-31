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
                string script = @"wrapForm();
                                Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(onEachRequest);
                                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(wrapForm);
                                function wrapForm() {
                                    if ($('.wb-frmvld').length === 0) {
                                        // wrap all the form with the class for the validation
                                         $('form').wrap('<div class=""wb-frmvld""></div>');
                                    }
                                }
                                function onEachRequest(sender, args) {
                                    var element = args.get_postBackElement();
                                    if ((element.type === 'submit' || element.hasAttribute('formsubmit')) && !element.hasAttribute('formnovalidate')) {
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
                                    $(':checkbox[data-rule-require_from_group]').closest('fieldset').find('div').on('change', function() {
                                        $(this).closest('fieldset').find(':checkbox[data-rule-require_from_group]').valid();
                                    });
                                };";

                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-checkboxlist", script, true);
            }
        }

        public static void ValidateScript(Page p, string clientID)
        {
            // validate a unique control after postback async or not
            string script = @"validateCtrl();
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(validateCtrl);
                            function validateCtrl() {
                               if (jQuery.validator && jQuery.validator !== 'undefined') {
                                    $(function(){ $('#" + clientID + @"').valid(); });
                                } else {
                                    $(document).on('wb-ready.wb', function(event) {
                                        $('#" + clientID + @"').valid();
                                    });
                                }
                            };";
            ScriptManager.RegisterStartupScript(p, typeof(string), "wb-frmvld-validate-" + clientID, script, true);
        }

        public static void InitDatePicker(Page p)
        {
            if (!p.ClientScript.IsStartupScriptRegistered("wb-frmvld-date"))
            {
                // postack event initialisation
                string initDate = @"Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initDatePicker);
                                function initDatePicker() {
                                    $('input[type=date]').trigger('wb-init.wb-date');
                                }";
                p.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-date", initDate, true);
            }
        }
    }
}
