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
        DefaultValue(1),
        ]
        public int MinNumberFieldsRequired
        {
            get
            {
                object o = ViewState["MinNumberFieldsRequired"];
                return (o == null) ? 1 : (int)o;
            }
            set
            {
                if (value < 0) throw new Exception("The value of 'MinNumberFieldsRequired' must be greater than 0");
                ViewState["MinNumberFieldsRequired"] = value;
            }
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

            // add startup init script
            WetControls.Extensions.ClientScript.InitScript(this.Page);

            if (EnableClientValidation && IsRequired)
            {
                // fix checkboxlist client validation
                WetControls.Extensions.ClientScript.FixCheckBoxList(this.Page);

                if (!string.IsNullOrEmpty(ValidationErrorMsg))
                {
                    base.Attributes.Add("data-msg", ValidationErrorMsg);
                }
            }
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
                if (repeatIndex == 0)
                {
                    writer.AddAttribute("data-rule-require_from_group", string.Format("[{0}, \".{1}\"]", this.MinNumberFieldsRequired, this.ClientID));
                }
            }
            base.RenderItem(itemType, repeatIndex, repeatInfo, writer);

            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
