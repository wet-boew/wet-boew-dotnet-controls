using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WetControls
{
    public static class WebControlExt
    {
        public static void RemoveCssClass(this System.Web.UI.WebControls.WebControl control, String css)
        {
            control.CssClass = String.Join(" ", control.CssClass.Split(' ').Where(x => x != css).ToArray());
        }

        public static void AddCssClass(this System.Web.UI.WebControls.WebControl control, String css)
        {
            control.RemoveCssClass(css);
            css += " " + control.CssClass;
            control.CssClass = css.Trim();
        }
    }
}
