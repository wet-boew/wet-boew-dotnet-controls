using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetRadioButton runat=\"server\"></{0}:WetRadioButton>")]
    public class WetRadioButton : RadioButton, WetControls.Interfaces.IWet
    {
        private string Lang { get { return System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName; } }
        private string RequiredText { get { return Lang == "fr" ? " (Obligatoire)" : " (Required)"; } }

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
        DefaultValue(false),
        ]
        public bool IsInline
        {
            get
            {
                object o = ViewState["IsInline"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsInline"] = value; }
        }
        public bool IsValid
        {
            get
            {
                object o = ViewState["IsValid"];
                if (o == null)
                {
                    return (Visible && IsRequired && !this.Checked) ? false : true;
                }
                else
                {
                    return (bool)o;
                }
            }
            set { ViewState["IsValid"] = value; }
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
        public bool IsPostBackEventControlRegistered
        {
            get
            {
                object o = ViewState["IsPostBackEventControlRegistered"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsPostBackEventControlRegistered"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // add startup init script
            WetControls.Extensions.ClientScript.InitScript(Page);

            base.Attributes.Clear();

            if (EnableClientValidation)
            {
                if (IsRequired)
                {
                    base.Attributes.Add("required", "required");
                }
                if (!string.IsNullOrEmpty(ValidationErrorMsg))
                {
                    base.Attributes.Add("data-msg", ValidationErrorMsg);
                }
                if (!IsPostBackEventControlRegistered)
                {
                    IsPostBackEventControlRegistered = this.Page.AutoPostBackControl == this;
                }
            }
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            if (IsPostBackEventControlRegistered && !this.IsValid)
            {
                // validate after postback
                WetControls.Extensions.ClientScript.ValidateScript(Page, this.ClientID);
            }
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            }
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
            

            writer.AddAttribute(HtmlTextWriterAttribute.Class, IsInline ? "radio-inline" : "radio", false);

            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            base.Render(writer);

            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
