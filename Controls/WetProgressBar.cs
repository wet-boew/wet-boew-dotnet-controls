using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("ProgressList"),
    ParseChildren(true, "ProgressList"),
    ToolboxData("<{0}:WetProgressBar runat=\"server\"></{0}:WetProgressBar>")]
    public class WetProgressBar : CompositeControl
    {
        private List<ProgressData> progressList;
        [
        Category("Behavior"),
        Description("The progress collection"),
        DesignerSerializationVisibility(
            DesignerSerializationVisibility.Content),
            Editor(typeof(ProgressCollectionEditor), 
            typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        public List<ProgressData> ProgressList
        {
            get 
            { 
                if (progressList == null)
                {
                    progressList = new List<ProgressData>();
                }
                return progressList;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (progressList != null && progressList.Count > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "progress");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                foreach (ProgressData item in progressList)
                {
                    if (item.MinWidth > 0)
                    {
                        writer.AddStyleAttribute("min-width", string.Format("{0}em", item.MinWidth));
                    }
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, string.Format("{0}%", (int)item.ProgressWidth));
                    writer.AddAttribute("aria-valuemax", item.ValueMax.ToString());
                    writer.AddAttribute("aria-valuemin", item.ValueMin.ToString());
                    writer.AddAttribute("aria-valuenow", item.ValueNow.ToString());
                    writer.AddAttribute("role", "progressbar");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, item.ProgressBarClass);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    if (!item.ShowValueLabel)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "sr-only");
                    }
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(string.Format("{0}%", item.ProgressWidth));
                    writer.RenderEndTag();

                    writer.RenderEndTag();
                }

                writer.RenderEndTag();
            }
        }
    }

    [ToolboxItem(false)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProgressData
    {
        public enum ENUM_PROGRESS_STYLE { Default = 0, Success = 1, Info = 2, Warning = 3, Danger = 4 };

        private decimal valueNow;
        private decimal valueMin;
        private decimal valueMax;
        private bool showValueLabel;
        private bool striped;
        private bool animated;
        private int minWidth;
        private ENUM_PROGRESS_STYLE progressStyle;

        public ProgressData()
            : this(0, 0, 0, false, false, false, 0, ENUM_PROGRESS_STYLE.Default)
        {
        }

        public ProgressData(int valueNow, int valueMin, int valueMax, bool showValueLabel, bool striped, bool animated, int minWidth, ENUM_PROGRESS_STYLE progressStyle)
        {
            this.valueNow = valueNow;
            this.valueMin = valueMin;
            this.valueMax = valueMax;
            this.showValueLabel = showValueLabel;
            this.striped = striped;
            this.animated = animated;
            this.minWidth = minWidth;
            this.progressStyle = progressStyle;
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        NotifyParentProperty(true),
        ]
        public decimal ValueNow
        {
            get { return valueNow; }
            set { valueNow = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        NotifyParentProperty(true),
        ]
        public decimal ValueMin
        {
            get { return valueMin; }
            set { valueMin = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        NotifyParentProperty(true),
        ]
        public decimal ValueMax
        {
            get { return valueMax; }
            set { valueMax = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool ShowValueLabel
        {
            get { return showValueLabel; }
            set { showValueLabel = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool Striped
        {
            get { return striped; }
            set { striped = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool Animated
        {
            get { return animated; }
            set { animated = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        ]
        public int MinWidth
        {
            get { return minWidth; }
            set { minWidth = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ENUM_PROGRESS_STYLE.Default),
        ]
        public ENUM_PROGRESS_STYLE ProgressStyle
        {
            get { return progressStyle; }
            set { progressStyle = value; }
        }

        public decimal ProgressWidth
        {
            get { return ValueNow / ValueMax * 100; }
        }

        public string ProgressBarClass
        {
            get
            {
                string css = "progress-bar";

                // style
                if (ProgressStyle == ENUM_PROGRESS_STYLE.Success)
                {
                    css += " progress-bar-success";
                }
                else if (ProgressStyle == ENUM_PROGRESS_STYLE.Info)
                {
                    css += " progress-bar-info";
                }
                else if (ProgressStyle == ENUM_PROGRESS_STYLE.Warning)
                {
                    css += " progress-bar-warning";
                }
                else if (ProgressStyle == ENUM_PROGRESS_STYLE.Danger)
                {
                    css += " progress-bar-danger";
                }

                // striped
                if (Striped)
                {
                    css += " progress-bar-striped";
                }

                // animated
                if (Animated)
                {
                    css += " active";
                }

                return css;
            }
        }
    }

    public class ProgressCollectionEditor : CollectionEditor
    {
        public ProgressCollectionEditor(Type type)
            : base(type)
        { 
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(ProgressData);
        }
    }
}
