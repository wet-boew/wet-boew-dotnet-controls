using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetImage runat=\"server\"></{0}:WetImage>")]
    public class WetImage : Image
    {
        public enum ENUM_SHAPE { Default = 0, Circle = 1, Rounded = 2, Thumbnail = 3 };

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ENUM_SHAPE.Default),
        ]
        public ENUM_SHAPE Shape
        {
            get
            {
                object o = ViewState["ButtonType"];
                return (o == null) ? ENUM_SHAPE.Default : (ENUM_SHAPE)o;
            }
            set { ViewState["ButtonType"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsResponsive
        {
            get
            {
                object o = ViewState["IsResponsive"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsResponsive"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Shape == ENUM_SHAPE.Circle)
            {
                this.AddCssClass("img-circle");
            }
            else if (Shape == ENUM_SHAPE.Rounded)
            {
                this.AddCssClass("img-rounded");
            }
            else if (Shape == ENUM_SHAPE.Thumbnail)
            {
                this.AddCssClass("img-thumbnail");
            }

            // responsive
            if (IsResponsive)
            {
                this.AddCssClass("img-responsive");
            }
        }
    }
}
