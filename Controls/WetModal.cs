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
        [
        Browsable(false),
        DefaultValue(null),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        private HtmlGenericControl _headerTemplate { get; set; }
        [
        Browsable(false),
        DefaultValue(null),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        private HtmlGenericControl _bodyTemplate { get; set; }
        [
        Browsable(false),
        DefaultValue(null),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        private HtmlGenericControl _footerTemplate { get; set; }

        [
        Browsable(true),
        TemplateInstance(TemplateInstance.Single),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public ITemplate HeaderTemplate { get; set; }

        [
        Browsable(true),
        TemplateInstance(TemplateInstance.Single),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public ITemplate BodyTemplate { get; set; }

        [
        Browsable(true),
        TemplateInstance(TemplateInstance.Single),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public ITemplate FooterTemplate { get; set; }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string AssociateControlID
        {
            get
            {
                string m = (string)ViewState["AssociateControlID"];
                return m ?? String.Empty;
            }
            set { ViewState["AssociateControlID"] = value; }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        ]
        public bool IsClosable
        {
            get
            {
                object o = ViewState["IsClosable"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["IsClosable"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            // startup init script
            WetControls.Extensions.ClientScript.InitScript(Page);

            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // header
            if (HeaderTemplate != null)
            {
                _headerTemplate = new HtmlGenericControl("header");
                _headerTemplate.Attributes.Add("class", "modal-header");
                HtmlGenericControl h2 = new HtmlGenericControl("h2");
                h2.Attributes.Add("class", "modal-title");
                _headerTemplate.Controls.Add(h2);
                this.Controls.Add(_headerTemplate);
                HeaderTemplate.InstantiateIn(h2);
            }

            // body with empty content by default
            _bodyTemplate = new HtmlGenericControl("div");
            _bodyTemplate.Attributes.Add("class", "modal-body");
            this.Controls.Add(_bodyTemplate);
            if (BodyTemplate != null)
            {
                BodyTemplate.InstantiateIn(_bodyTemplate);
            }

            // footer
            if (FooterTemplate != null)
            {
                _footerTemplate = new HtmlGenericControl("div");
                _footerTemplate.Attributes.Add("class", "modal-footer");
                this.Controls.Add(_footerTemplate);
                FooterTemplate.InstantiateIn(_footerTemplate);
            }

            try
            {
                Control target = Page.FindControlRecursive(this.AssociateControlID);

                if (target is WebControl wc)
                {
                    wc.Attributes["href"] = "#" + this.ClientID;
                    wc.AddCssClass(IsClosable ? "wb-lbx" : "wb-lbx lbx-modal");
                }
            }
            catch (Exception)
            {
                throw new Exception("AssociateControlID property for WetModal with ID " + this.ID + " should refer to an html control with the href attribute.");
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "mfp-hide modal-dialog modal-content overlay-def");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag("section");
            if (_headerTemplate != null) _headerTemplate.RenderControl(writer);
            _bodyTemplate.RenderControl(writer); // empty by default
            if (_footerTemplate != null) _footerTemplate.RenderControl(writer);
            writer.RenderEndTag();
        }
    }
}