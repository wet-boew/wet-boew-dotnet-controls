using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetModal runat=\"server\"></{0}:WetModal>")]
    public class WetModal : WebControl, INamingContainer
    {
        private ITemplate _contentTemplate = null;

        [
        Browsable(true),
        DefaultValue(null),
        TemplateContainer(typeof(TemplateContainer)),
        TemplateInstance(TemplateInstance.Single),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public ITemplate ContentTemplate
        {
            get { return _contentTemplate; }
            set { _contentTemplate = value; }
        }

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
                return t ?? "";
            }
            set { ViewState["HeaderText"] = value; }
        }

        public void ShowPopup()
        {
            string script = @"$(function(){
                                $('#" + this.ClientID + @"').appendTo($('form'));
                                $(document).trigger('open.wb-lbx', [[{src:'#" + this.ClientID + @"', type:'inline'}]]);
                            });";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "wetModal", script, true);
        }

        protected override void CreateChildControls()
        {
            if (ContentTemplate != null)
            {
                Controls.Clear();
                TemplateContainer container = new TemplateContainer();
                ContentTemplate.InstantiateIn(container);
                Controls.Add(container);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "mfp-hide modal-dialog modal-content overlay-def");
            writer.RenderBeginTag("section");
            if (!string.IsNullOrEmpty(HeaderText))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-header");
                writer.RenderBeginTag("header");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-title");
                writer.RenderBeginTag(HtmlTextWriterTag.H2);
                writer.Write(this.HeaderText);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }

    public class TemplateContainer : WebControl, INamingContainer {}
}
