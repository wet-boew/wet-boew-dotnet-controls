using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetModal runat=\"server\"></{0}:WetModal>")]
    public class WetModal : WebControl, INamingContainer
    {
        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        DefaultValue(null),
        Description("ItemTemplate")
        ]
        private PlaceHolder _content { get; set; }

        [
        PersistenceMode(PersistenceMode.InnerProperty),
        TemplateInstance(TemplateInstance.Single),
        ]
        public ITemplate Content { get; set; }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string HeaderText
        {
            get
            {
                string t = (string)ViewState["HeaderText"];
                return (t == null) ? String.Empty : t;
            }
            set { ViewState["HeaderText"] = value; }
        }

        public void ShowPopup()
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "wetModal", "$(function(){wb.doc.trigger( 'open.wb-lbx', [[{src:'#" + this.ClientID + "', type:'inline'}]]);});", true);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _content = new PlaceHolder();
            Content.InstantiateIn(_content);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            foreach (Control ctrl in this.Controls)
            {
                _content.Controls.Add(ctrl);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "wb-overlay modal-content overlay-def wb-popup-mid");
            writer.RenderBeginTag("section");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-header");
            writer.RenderBeginTag("header");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-title");
            writer.RenderBeginTag(HtmlTextWriterTag.H2);
            writer.Write(this.HeaderText);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            _content.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
