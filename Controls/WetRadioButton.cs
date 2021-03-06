﻿using System;
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

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public new string ID
        {
            get { return base.ID; }
            set { base.ID = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(null),
        ]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public new string CssClass
        {
            get { return base.CssClass; }
            set { base.CssClass = value; }
        }
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
                return t ?? String.Empty;
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
                return t ?? String.Empty;
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
                return t ?? String.Empty;
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

        protected override void OnLoad(EventArgs e)
        {
            // startup init script
            WetControls.Extensions.ClientScript.InitScript(Page);

            if (EnableClientValidation && IsRequired)
            {
                base.Attributes.Add("required", "required");

                if (!string.IsNullOrEmpty(ValidationErrorMsg))
                {
                    base.Attributes.Add("data-msg", ValidationErrorMsg);
                }
            }

            base.OnLoad(e);
        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page.AutoPostBackControl == this)
            {
                // validate after postback
                WetControls.Extensions.ClientScript.ValidateScript(Page);
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

            base.Render(writer);

            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        public void Clear()
        {
            this.Checked = false;
            // reset validation
            ViewState["IsValid"] = null;
        }
    }
}
