﻿using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetDropDownList runat=\"server\"></{0}:WetDropDownList>")]
    public class WetDropDownList : DropDownList, WetControls.Interfaces.IWet
    {
        private string Lang { get { return System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName; } }
        private string RequiredText { get { return Lang == "fr" ? " (Obligatoire)" : " (Required)"; } }
        private string SelectText { get { return Lang == "fr" ? "== Veuillez sélectionner ==" : "== Please Select =="; } }

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
        DefaultValue(true),
        ]
        public bool EnableSelectText
        {
            get
            {
                object o = ViewState["EnableSelectText"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["EnableSelectText"] = value; }
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

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        ]
        public bool AutoSelectUniqueItem
        {
            get
            {
                object o = ViewState["AutoSelectUniqueItem"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["AutoSelectUniqueItem"] = value; }
        }

        public bool IsValid
        {
            get 
            { 
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

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            // select first item if unique
            if (AutoSelectUniqueItem && this.Items.Count == 1)
            {
                this.Items[0].Selected = true;
            }

            if (EnableSelectText)
            {
                // add first item text
                ListItem li = new ListItem() { Value = "", Text = this.SelectText };
                li.Attributes.Add("label", this.SelectText);
                if (!this.Items.Contains(li))
                {
                    this.Items.Insert(0, li);
                }
            }
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
     
            this.AddCssClass("form-control");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page.AutoPostBackControl == this)
            {
                // validate after postback
                WetControls.Extensions.ClientScript.ValidateScript(Page);
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (IsRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "required", false);
            }
            // create and render title
            writer.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            if (string.IsNullOrEmpty(LabelCssClass))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "field-name", false);
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "field-name " + LabelCssClass, false);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(LabelText);
            writer.RenderEndTag();
            if (IsRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "required", false);
                writer.RenderBeginTag(HtmlTextWriterTag.Strong);
                writer.Write(RequiredText);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            // call the base class's Render method
            base.Render(writer);

            writer.RenderEndTag();
        }

        public void Clear()
        {
            this.ClearSelection();
            // reset validation
            ViewState["IsValid"] = null;
        }
    }
}
