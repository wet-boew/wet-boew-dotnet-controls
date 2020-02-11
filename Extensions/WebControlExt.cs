using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace WetControls
{
    public static class WebControlExt
    {
        /// <summary>
        /// Remove class from the WebControl
        /// </summary>
        /// <param name="id">Class name in string format</param>
        public static void RemoveCssClass(this System.Web.UI.WebControls.WebControl control, String css)
        {
            control.CssClass = String.Join(" ", control.CssClass.Split(' ').Where(x => x != css).ToArray());
        }

        /// <summary>
        /// Add class to the WebControl
        /// </summary>
        /// <param name="id">Class name in string format</param>
        public static void AddCssClass(this System.Web.UI.WebControls.WebControl control, String css)
        {
            control.RemoveCssClass(css);
            css += " " + control.CssClass;
            control.CssClass = css.Trim();
        }

        /// <summary>
        /// Recursively searches for a server control with the given ID.
        /// </summary>
        /// <param name="id">ID of control to find</param>
        /// <returns>The matching control or null if no match was found</returns>
        public static Control FindControlRecursive(this Control control, string id)
        {
            foreach (Control ctl in control.Controls)
            {
                if (ctl.ID == id)
                    return ctl;

                Control child = FindControlRecursive(ctl, id);
                if (child != null)
                    return child;
            }
            return null;
        }
    }
}

public static class PageExt
{
    /// <summary>
    /// Validate all IWet Controls in the page.
    /// </summary>
    /// <returns>Return validation in boolean format</returns>
    public static bool IsWetValid(this Page page)
    {
        bool IsAllIWetCtrlsValid = true;

        foreach (var ctrl in page.GetAllIWetControls())
        {
            if (!ctrl.IsValid)
            {
                IsAllIWetCtrlsValid = false;
                break;
            }
        }

        return IsAllIWetCtrlsValid;
    }

    /// <summary>
    /// Clear all IWet child controls from its parent
    /// </summary>
    public static void ClearAllInnerWetControls(this Control ctrls)
    {
        foreach (var ctrl in ctrls.GetAllIWetControls())
        {
            ctrl.Clear();
        }
    }

    public static IEnumerable<WetControls.Interfaces.IWet> GetAllIWetControls(this Control parent)
    {
        var result = new List<WetControls.Interfaces.IWet>();
        foreach (Control control in parent.Controls)
        {
            if (control is WetControls.Interfaces.IWet)
            {
                result.Add((WetControls.Interfaces.IWet)control);
            }
            if (control.HasControls())
            {
                result.AddRange(control.GetAllIWetControls());
            }
        }
        return result;
    }
}

