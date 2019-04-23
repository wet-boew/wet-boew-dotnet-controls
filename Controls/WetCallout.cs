using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("WetControls.StyleSheets.bs-callout.css", "text/css")]
namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetCallout runat=\"server\"></{0}:WetCallout>")]
    public class WetCallout : Panel
    {
        public enum ENUM_CALLOUT_TYPE { Default = 0, Primary = 1, Success = 2, Info = 3, Warning = 4, Danger = 5 };

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string Title
        {
            get
            {
                string t = (string)ViewState["Title"];
                return (t == null) ? String.Empty : t;
            }
            set { ViewState["Title"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ENUM_CALLOUT_TYPE.Default),
        ]
        public ENUM_CALLOUT_TYPE Type
        {
            get
            {
                object o = ViewState["Type"];
                return (o == null) ? ENUM_CALLOUT_TYPE.Default : (ENUM_CALLOUT_TYPE)o;
            }
            set { ViewState["Type"] = value; }
        }

        private void RegisterCustomCss()
        {
            // add embedded style sheet to parent page
            System.Web.UI.HtmlControls.HtmlLink cssLink = new System.Web.UI.HtmlControls.HtmlLink();
            cssLink.ID = "bs-callout-css";
            cssLink.Href = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WetControls.StyleSheets.bs-callout.css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");

            bool CssLinkAlreadyExists = false;
            foreach (Control headctrl in Page.Header.Controls)
            {
                if (headctrl.ID == cssLink.ID)
                {
                    CssLinkAlreadyExists = true;
                    break;
                }
            }

            if (!CssLinkAlreadyExists)
            {
                Page.Header.Controls.AddAt(0, cssLink);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.RegisterCustomCss();

            if (Type == ENUM_CALLOUT_TYPE.Primary)
            {
                this.AddCssClass("bs-callout-primary");
            }
            else if (Type == ENUM_CALLOUT_TYPE.Success)
            {
                this.AddCssClass("bs-callout-success");
            }
            else if (Type == ENUM_CALLOUT_TYPE.Info)
            {
                this.AddCssClass("bs-callout-info");
            }
            else if (Type == ENUM_CALLOUT_TYPE.Warning)
            {
                this.AddCssClass("bs-callout-warning");
            }
            else if (Type == ENUM_CALLOUT_TYPE.Danger)
            {
                this.AddCssClass("bs-callout-danger");
            }
            else
            {
                this.AddCssClass("bs-callout-default");
            }

            this.AddCssClass("bs-callout");

            // title
            if (!string.IsNullOrEmpty(Title))
            {
                LiteralControl h4 = new LiteralControl()
                {
                    Text = "<h4>" + Title + "</h4>"
                };
                this.Controls.AddAt(0, h4);
            }
        }
    }
}
