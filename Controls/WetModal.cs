using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetModal runat=\"server\"></{0}:WetModal>")]
    public class WetModal : WebControl, INamingContainer
    {
        [
        Browsable(true),
        DefaultValue(null),
        TemplateInstance(TemplateInstance.Single),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public ITemplate HeaderTemplate { get; set; }

        [
        Browsable(true),
        DefaultValue(null),
        TemplateInstance(TemplateInstance.Single),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public ITemplate BodyTemplate { get; set; }

        [
        Browsable(true),
        DefaultValue(null),
        TemplateInstance(TemplateInstance.Single),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public ITemplate FooterTemplate { get; set; }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            if (HeaderTemplate != null)
            {
                HtmlGenericControl header = new HtmlGenericControl("header");
                header.Attributes.Add("class", "modal-header");
                HtmlGenericControl h2 = new HtmlGenericControl("h2");
                h2.Attributes.Add("class", "modal-title");
                header.Controls.Add(h2);

                HeaderTemplate.InstantiateIn(h2);
                Controls.Add(header);
            }
            if (BodyTemplate != null)
            {
                HtmlGenericControl body = new HtmlGenericControl("div");
                body.Attributes.Add("class", "modal-body");

                BodyTemplate.InstantiateIn(body);
                Controls.Add(body);
            }
            if (FooterTemplate != null)
            {
                HtmlGenericControl footer = new HtmlGenericControl("div");
                footer.Attributes.Add("class", "modal-footer");
                FooterTemplate.InstantiateIn(footer);
                Controls.Add(footer);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "mfp-hide modal-dialog modal-content overlay-def");
            //writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag("section");
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
