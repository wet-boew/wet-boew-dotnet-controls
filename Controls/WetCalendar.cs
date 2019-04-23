using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: System.Web.UI.WebResource("WetControls.StyleSheets.fullcalendar.min.css", "text/css")]
[assembly: System.Web.UI.WebResource("WetControls.StyleSheets.fullcalendar.print.min.css", "text/css")]
[assembly: System.Web.UI.WebResource("WetControls.StyleSheets.jquery.qtip.min.css", "text/css")]
[assembly: System.Web.UI.WebResource("WetControls.StyleSheets.jquery-ui.min.css", "text/css")]
[assembly: System.Web.UI.WebResource("WetControls.Scripts.moment.min.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("WetControls.Scripts.fullcalendar.min.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("WetControls.Scripts.locale-all.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("WetControls.Scripts.jquery.qtip.min.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("WetControls.Scripts.jquery-ui.min.js", "text/javascript")]
namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetCalendar runat=\"server\"></{0}:WetCalendar>")]
    public class WetCalendar : WebControl
    {
        public enum DEFAULT_VIEW { month = 1 , agendaWeek, agendaDay, listMonth };


        [Bindable(true), Category("Appearance"), DefaultValue(DEFAULT_VIEW.agendaWeek)]
        public DEFAULT_VIEW DefaultView
        {
            get
            {
                object o = ViewState["DefaultView"];
                return (o == null) ? DEFAULT_VIEW.agendaWeek : (DEFAULT_VIEW)o;
            }
            set { ViewState["DefaultView"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        public bool AllDaySlot
        {
            get
            {
                object o = ViewState["AllDaySlot"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["AllDaySlot"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        public bool Weekends
        {
            get
            {
                object o = ViewState["Weekends"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["Weekends"] = value; }
        }

        [Bindable(true), Category("Appearance")]
        public TimeSpan MinTime
        {
            get
            {
                object o = ViewState["MinTime"];
                return (o == null) ? new TimeSpan(0, 0, 0) : (TimeSpan)o;
            }
            set { ViewState["MinTime"] = value; }
        }

        [Bindable(true), Category("Appearance")]
        public TimeSpan MaxTime
        {
            get
            {
                object o = ViewState["MaxTime"];
                return (o == null) ? new TimeSpan(24, 0, 0) : (TimeSpan)o;
            }
            set { ViewState["MaxTime"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(30)]
        public int DefaultTimedEventDuration
        {
            get
            {
                object o = ViewState["DefaultTimedEventDuration"];
                return (o == null) ? 30 : (int)o;
            }
            set { ViewState["DefaultTimedEventDuration"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(0)]
        public int ContentHeight
        {
            get
            {
                object o = ViewState["ContentHeight"];
                return (o == null) ? 0 : (int)o;
            }
            set { ViewState["ContentHeight"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        public bool Selectable
        {
            get
            {
                object o = ViewState["Selectable"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["Selectable"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(true)]
        public bool LazyFetching
        {
            get
            {
                object o = ViewState["LazyFetching"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["LazyFetching"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("start")]
        public string StartParam
        {
            get
            {
                object o = ViewState["StartParam"];
                return (o == null) ? "sart" : o.ToString();
            }
            set { ViewState["StartParam"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("end")]
        public string EndParam
        {
            get
            {
                object o = ViewState["EndParam"];
                return (o == null) ? "end" : o.ToString();
            }
            set { ViewState["EndParam"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        public bool SlotEventOverlap
        {
            get
            {
                object o = ViewState["SlotEventOverlap"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["SlotEventOverlap"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("prev,next,today")]
        public string HeaderLeft
        {
            get
            {
                object o = ViewState["HeaderLeft"];
                return (o == null) ? "prev,next,today" : o.ToString();
            }
            set { ViewState["HeaderLeft"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("title")]
        public string HeaderCenter
        {
            get
            {
                object o = ViewState["HeaderCenter"];
                return (o == null) ? "title" : o.ToString();
            }
            set { ViewState["HeaderCenter"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("month,agendaWeek,agendaDay,listMonth")]
        public string HeaderRight
        {
            get
            {
                object o = ViewState["HeaderRight"];
                return (o == null) ? "month,agendaWeek,agendaDay,listMonth" : o.ToString();
            }
            set { ViewState["HeaderRight"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("dddd DD")]
        public string ViewWeekColumnFormat
        {
            get
            {
                object o = ViewState["ViewWeekColumnFormat"];
                return (o == null) ? "dddd DD" : o.ToString();
            }
            set { ViewState["ViewWeekColumnFormat"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("local")]
        public string Timezone
        {
            get
            {
                object o = ViewState["Timezone"];
                return (o == null) ? "local" : o.ToString();
            }
            set { ViewState["Timezone"] = value; }
        }

        private void RegisterComponentScripts()
        {
            System.Web.UI.ScriptManager sm = System.Web.UI.ScriptManager.GetCurrent(Page);

            if (sm == null)
                throw new Exception("Vous devez mettre un script manager dans votre page.");

            sm.Scripts.Add(new System.Web.UI.ScriptReference("WetControls.Scripts.moment.min.js", "WetControls"));
            sm.Scripts.Add(new System.Web.UI.ScriptReference("WetControls.Scripts.jquery-ui.min.js", "WetControls"));
            sm.Scripts.Add(new System.Web.UI.ScriptReference("WetControls.Scripts.jquery.qtip.min.js", "WetControls"));
            sm.Scripts.Add(new System.Web.UI.ScriptReference("WetControls.Scripts.fullcalendar.min.js", "WetControls"));
            sm.Scripts.Add(new System.Web.UI.ScriptReference("WetControls.Scripts.locale-all.js", "WetControls"));

            // fullcalendar css
            Literal fullcalendarCssLink = new Literal();
            fullcalendarCssLink.ID = "fullcalendarCssLink";
            fullcalendarCssLink.Text = string.Format("<link rel='stylesheet' type='text/css' href='{0}'/>", Page.ClientScript.GetWebResourceUrl(this.GetType(), "WetControls.StyleSheets.fullcalendar.min.css"));
            InsertCssLink(fullcalendarCssLink);

            // fullcalendar print css
            Literal fullcalendarPrintCssLink = new Literal();
            fullcalendarPrintCssLink.ID = "fullcalendarPrintCssLink";
            fullcalendarPrintCssLink.Text = string.Format("<link rel='stylesheet' type='text/css' href='{0}'/>", Page.ClientScript.GetWebResourceUrl(this.GetType(), "WetControls.StyleSheets.fullcalendar.print.min.css"));
            InsertCssLink(fullcalendarPrintCssLink);

            // jquery qtip css
            Literal qtipCssLink = new Literal();
            qtipCssLink.ID = "qtipCssLink";
            qtipCssLink.Text = string.Format("<link rel='stylesheet' type='text/css' href='{0}'/>", Page.ClientScript.GetWebResourceUrl(this.GetType(), "WetControls.StyleSheets.jquery.qtip.min.css"));
            InsertCssLink(qtipCssLink);

            // jquery ui
            Literal jQueryUICssLink = new Literal();
            jQueryUICssLink.ID = "jQueryUICssLink";
            jQueryUICssLink.Text = string.Format("<link rel='stylesheet' type='text/css' href='{0}'/>", Page.ClientScript.GetWebResourceUrl(this.GetType(), "WetControls.StyleSheets.jquery-ui.min.css"));
            InsertCssLink(jQueryUICssLink);
            
        }

        private void InsertCssLink(Literal csslink)
        {
            bool cssExist = false;
            foreach (System.Web.UI.Control control in Page.Header.Controls)
            {
                if (control is System.Web.UI.WebControls.Literal && control.ID == csslink.ID)
                {
                    cssExist = true;
                    break;
                }
            }

            if (!cssExist)
            {
                Page.Header.Controls.Add(csslink);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.RegisterComponentScripts();

            base.OnPreRender(e);

            // init calender
            string script = string.Format(@"init_{0} = function(){{
                                                $('#{0}').fullCalendar({{
                                                    locale: '{1}',
                                                    defaultView: '{2}',
                                                    allDaySlot: {3},
                                                    slotEventOverlap: {4},
                                                    weekends: {5},
                                                    minTime: '{6}',
                                                    maxTime: '{7}',
                                                    defaultTimedEventDuration: '{8}',
                                                    contentHeight: {9},
                                                    selectable: {10},
                                                    timezone: '{11}',
                                                    header: {{
                                                        left: '{12}',
                                                        center: '{13}',
                                                        right: '{14}'
                                                    }},
                                                    views: {{
                                                        week: {{
                                                            columnFormat: '{15}'
                                                        }}
                                                    }}
                                                }});
                                            }}; 
                                            $(init_{0}); 
                                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(init_{0});", this.ClientID,
                                                                                                                      System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower(),
                                                                                                                      this.DefaultView.ToString(),
                                                                                                                      this.AllDaySlot.ToString().ToLower(),
                                                                                                                      this.SlotEventOverlap.ToString().ToLower(),
                                                                                                                      this.Weekends.ToString().ToLower(),
                                                                                                                      this.MinTime,
                                                                                                                      this.MaxTime,
                                                                                                                      this.DefaultTimedEventDuration.ToString(),
                                                                                                                      this.ContentHeight == 0 ? "'auto'" : this.ContentHeight.ToString(),
                                                                                                                      this.Selectable.ToString().ToLower(),
                                                                                                                      this.Timezone,
                                                                                                                      this.HeaderLeft,
                                                                                                                      this.HeaderCenter,
                                                                                                                      this.HeaderRight,
                                                                                                                      this.ViewWeekColumnFormat);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "calendar_" + this.ClientID, script, true);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
        }
    }
}
