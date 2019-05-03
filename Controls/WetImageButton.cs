using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetImageButton runat=\"server\"></{0}:WetImageButton>")]
    public class WetImageButton : ImageButton
    {
        public enum ENUM_TYPE { Default = 0, Primary = 1, Success = 2, Info = 3, Warning = 4, Danger = 5, NoColor = 6, Link = 7 };
        public enum ENUM_SIZE { Default = 0, Extrasmall = 1, Small = 2, Large = 3 };

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ENUM_TYPE.Default),
        ]
        public ENUM_TYPE ButtonType
        {
            get
            {
                object o = ViewState["ButtonType"];
                return (o == null) ? ENUM_TYPE.Default : (ENUM_TYPE)o;
            }
            set { ViewState["ButtonType"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ENUM_SIZE.Default),
        ]
        public ENUM_SIZE ButtonSize
        {
            get
            {
                object o = ViewState["ButtonSize"];
                return (o == null) ? ENUM_SIZE.Default : (ENUM_SIZE)o;
            }
            set { ViewState["ButtonSize"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsFullWidth
        {
            get
            {
                object o = ViewState["IsFullWidth"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsFullWidth"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        ]
        public bool IsActive
        {
            get
            {
                object o = ViewState["IsActive"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsActive"] = value; }
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
        DefaultValue(""),
        ]
        public string MessageConfirmation
        {
            get
            {
                string m = (string)ViewState["MessageConfirmation"];
                return (m == null) ? String.Empty : m;
            }
            set { ViewState["MessageConfirmation"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // add startup init script
            WetControls.Extensions.ClientScript.InitScript(Page);

            this.AddCssClass("btn");

            this.Attributes.Add("formsubmit", "formsubmit");

            // type
            if (ButtonType == ENUM_TYPE.Primary)
            {
                this.AddCssClass("btn-primary");
            }
            else if (ButtonType == ENUM_TYPE.Success)
            {
                this.AddCssClass("btn-success");
            }
            else if (ButtonType == ENUM_TYPE.Info)
            {
                this.AddCssClass("btn-info");
            }
            else if (ButtonType == ENUM_TYPE.Warning)
            {
                this.AddCssClass("btn-warning");
            }
            else if (ButtonType == ENUM_TYPE.Danger)
            {
                this.AddCssClass("btn-danger");
            }
            else if (ButtonType == ENUM_TYPE.NoColor)
            {
                this.AddCssClass("btn-nocolor");
            }
            else if (ButtonType == ENUM_TYPE.Link)
            {
                this.AddCssClass("btn-link");
            }
            else
            {
                this.AddCssClass("btn-default");
            }

            // size
            if (ButtonSize == ENUM_SIZE.Extrasmall)
            {
                this.AddCssClass("btn-xs");
            }
            else if (ButtonSize == ENUM_SIZE.Small)
            {
                this.AddCssClass("btn-sm");
            }
            else if (ButtonSize == ENUM_SIZE.Large)
            {
                this.AddCssClass("btn-lg");
            }

            // full width
            if (IsFullWidth)
            {
                this.AddCssClass("btn-block");
            }

            // active
            if (IsActive)
            {
                this.AddCssClass("active");
            }

            if (!string.IsNullOrEmpty(MessageConfirmation))
            {
                // confirmation message
                this.OnClientClick = string.Format("return confirm('{0}');", this.MessageConfirmation.Replace('\'', '‘')) + this.OnClientClick;
            }

            if (!EnableClientValidation)
            {
                this.Attributes.Add("formnovalidate", "formnovalidate");
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
