using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetCheckBoxList runat=\"server\"></{0}:WetCheckBoxList>")]
    public class WetCheckBoxList : CheckBoxList, WetControls.Interfaces.IWet
    {
        private string Lang { get { return System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName; } }
        private string RequiredText { get { return Lang == "fr" ? " (Obligatoire)" : " (Required)"; } }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string LabelText
        {
            get
            {
                string t = (string)ViewState["LabelText"];
                return (t == null) ? String.Empty : t;
            }
            set { ViewState["LabelText"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string LabelCssClass
        {
            get
            {
                string t = (string)ViewState["LabelCssClass"];
                return (t == null) ? String.Empty : t;
            }
            set { ViewState["LabelCssClass"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string ValidationErrorMsg
        {
            get
            {
                string t = (string)ViewState["ValidationErrorMsg"];
                return (t == null) ? String.Empty : t;
            }
            set { ViewState["ValidationErrorMsg"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsRequired
        {
            get
            {
                object o = ViewState["IsRequired"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsRequired"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        ]
        public bool EnableClientValidation
        {
            get
            {
                object o = ViewState["EnableClientValidation"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["EnableClientValidation"] = value; }
        }

        public bool IsValid
        {
            get {
                object o = ViewState["IsValid"];
                if (o == null)
                {
                    return (Visible && IsRequired && string.IsNullOrEmpty(this.SelectedValue)) ? false : true; 
                }
                else
                {
                    return (bool)o;
                }
            }
            set { ViewState["IsValid"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // wrap all the form with the class for the validation
            if (!Page.ClientScript.IsStartupScriptRegistered("wb-frmvld-wrap"))
            {
                string wrap = @"if ($('.wb-frmvld').length === 0)
                                    $('body').wrapInner('<div class=""wb-frmvld""></div>');

                                Sys.Application.add_init(function () {
                                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                                    prm.add_initializeRequest(onEachRequest);
                                });

                                function onEachRequest(sender, args) {
                                    args.set_cancel(!$('form').valid());
                                };";

                Page.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-wrap", wrap, true);
            }

            // bug correction with checkboxlist because we can not put the same name for required validation in web form
            if (!Page.ClientScript.IsStartupScriptRegistered("wb-frmvld-checkboxlist"))
            {
                string chklist = @" fixCheckBoxList();
                                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(fixCheckBoxList);
                                    function fixCheckBoxList() {
                                        $(':checkbox[data-rule-require_from_group]').closest('.checkbox').siblings('div').on('change', function (evt) {
                                            var input = $(this).closest('fieldset').find(':checkbox[data-rule-require_from_group]');
                                            alert('test');
                                            $(this).closest('form').data('validator').element(input);
                                        });
                                    }";

                Page.ClientScript.RegisterStartupScript(typeof(string), "wb-frmvld-checkboxlist", chklist, true);
            }

            if (!string.IsNullOrEmpty(ValidationErrorMsg))
            {
                base.Attributes.Add("data-msg", ValidationErrorMsg);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);

            if (IsRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "required", false);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Legend);
            if (string.IsNullOrEmpty(LabelCssClass))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "field-name", false);
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "field-name " + LabelCssClass, false);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            if (!string.IsNullOrEmpty(LabelText))
            {
                writer.Write(LabelText);
            }
            writer.RenderEndTag();

            if (IsRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "required", false);
                writer.RenderBeginTag(HtmlTextWriterTag.Strong);
                writer.Write(RequiredText);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            for (int i = 0; i < this.Items.Count; i++)
            {
                this.RenderItem(ListItemType.Item, i, new RepeatInfo(), writer);
            }

            writer.RenderEndTag();
        }

        protected override void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, RepeatDirection == System.Web.UI.WebControls.RepeatDirection.Horizontal ? "checkbox-inline" : "checkbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            // required
            if (IsRequired && EnableClientValidation)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.ClientID);
            }
            if (repeatIndex == 0 && IsRequired && EnableClientValidation)
            {
                writer.AddAttribute("data-rule-require_from_group", "[1, \"." + this.ClientID + "\"]");
            }
            base.RenderItem(itemType, repeatIndex, repeatInfo, writer);

            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
