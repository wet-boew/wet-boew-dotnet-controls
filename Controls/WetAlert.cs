using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetAlert runat=\"server\"></{0}:WetAlert>")]
    public class WetAlert : Panel
    {
        public enum ALERT_TYPE { Info = 0, Success = 1, Warning = 2, Danger = 3 };

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string Title
        {
            get
            {
                string t = (string)ViewState["AlertTitle"];
                return t ?? String.Empty;
            }
            set { ViewState["AlertTitle"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string Content
        {
            get
            {
                object o = ViewState["AlertContent"];
                return (o == null) ? String.Empty : o.ToString();
            }
            set { ViewState["AlertContent"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ALERT_TYPE.Info),
        ]
        public ALERT_TYPE AlertType
        {
            get
            {
                object o = ViewState["AlertType"];
                return (o == null) ? ALERT_TYPE.Info : (ALERT_TYPE)o;
            }
            set { ViewState["AlertType"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool Dismissible
        {
            get
            {
                object o = ViewState["Dismissible"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["Dismissible"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.AddCssClass("alert");

            // alert type
            if (AlertType == ALERT_TYPE.Success)
            {
                this.AddCssClass("alert-success");
            }
            else if (AlertType == ALERT_TYPE.Warning)
            {
                this.AddCssClass("alert-warning");
            }
            else if (AlertType == ALERT_TYPE.Danger)
            {
                this.AddCssClass("alert-danger");
            }
            else
            {
                this.AddCssClass("alert-info");
            }

            // text
            if (!string.IsNullOrEmpty(Content))
            {
                this.Controls.AddAt(0, new LiteralControl()
                {
                    Text = "<p>" + Content + "</p>"
                });
            }

            // title
            if (!string.IsNullOrEmpty(Title))
            {
                this.Controls.AddAt(0, new LiteralControl()
                {
                    Text = "<strong>" + Title + "</strong>"
                });
            }

            // dismissible
            if (Dismissible)
            {
                this.AddCssClass("alert-dismissible");

                this.Controls.AddAt(0, new LiteralControl()
                {
                    Text = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>"
                });
            }

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute("role", "alert", false);

            // call the base class's Render method
            base.Render(writer);
        }
    }
}
