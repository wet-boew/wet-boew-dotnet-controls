﻿using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WetControls.Controls
{
    [ToolboxData("<{0}:WetTextBox runat=\"server\"></{0}:WetTextBox>")]
    public class WetTextBox : TextBox, WetControls.Interfaces.IWet
    {
        private string Lang { get { return System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName; } }
        private string RequiredText { get { return Lang == "fr" ? " (Obligatoire)" : " (Required)"; } }
        private string ErrorPrice { get { return Lang == "fr" ? "Veuillez spécifier un montant valide." : "Please specify a valid amount."; } }
        private string ErrorGovEmail { get { return Lang == "fr" ? "Veuillez fournir une adresse électronique du gouvernement valide." : "Please enter a valid government email address."; } }

        public enum ENUM_GROUP_SIZE { Default = 0, Small = 1, Large = 2 };

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public new string ID
        {
            get { return base.ID; }
            set { base.ID = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(null),
        ]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public new string CssClass
        {
            get { return base.CssClass; }
            set { base.CssClass = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string LabelText
        {
            get
            {
                string t = (string)ViewState["LabelText"];
                return t ?? String.Empty;
            }
            set { ViewState["LabelText"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string LabelCssClass
        {
            get
            {
                string t = (string)ViewState["LabelCssClass"];
                return t ?? String.Empty;
            }
            set { ViewState["LabelCssClass"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string Placeholder
        {
            get
            {
                string t = (string)ViewState["Placeholder"];
                return t ?? String.Empty;
            }
            set { ViewState["Placeholder"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string ValidationErrorMsg
        {
            get
            {
                string t = (string)ViewState["ValidationErrorMsg"];
                return t ?? String.Empty;
            }
            set { ViewState["ValidationErrorMsg"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ENUM_GROUP_SIZE.Default),
        ]
        public ENUM_GROUP_SIZE GroupSize
        {
            get
            {
                object o = ViewState["GroupSize"];
                return (o == null) ? ENUM_GROUP_SIZE.Default : (ENUM_GROUP_SIZE)o;
            }
            set { ViewState["GroupSize"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsRequired
        {
            get
            {
                object o = ViewState["IsRequired"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsRequired"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsPhoneNumber
        {
            get
            {
                object o = ViewState["IsPhoneNumber"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsPhoneNumber"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsPostalCode
        {
            get
            {
                object o = ViewState["IsPostalCode"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsPostalCode"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsEmail
        {
            get
            {
                object o = ViewState["IsEmail"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsEmail"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsGovernmentEmail
        {
            get
            {
                object o = ViewState["IsGovernmentEmail"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsGovernmentEmail"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsUrl
        {
            get
            {
                object o = ViewState["IsUrl"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsUrl"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsDate
        {
            get
            {
                object o = ViewState["IsDate"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsDate"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsTime
        {
            get
            {
                object o = ViewState["IsTime"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsTime"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsNumber
        {
            get
            {
                object o = ViewState["IsNumber"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsNumber"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        ]
        public int MinNumber
        {
            get
            {
                object o = ViewState["MinNumber"];
                return (o == null) ? 0 : (int)o;
            }
            set { ViewState["MinNumber"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        ]
        public int MaxNumber
        {
            get
            {
                object o = ViewState["MaxNumber"];
                return (o == null) ? 0 : (int)o;
            }
            set { ViewState["MaxNumber"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string MinDate
        {
            get
            {
                string t = (string)ViewState["MinDate"];
                return t ?? String.Empty;
            }
            set { ViewState["MinDate"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string MaxDate
        {
            get
            {
                string t = (string)ViewState["MaxDate"];
                return t ?? String.Empty;
            }
            set { ViewState["MaxDate"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        ]
        public decimal StepNumber
        {
            get
            {
                object o = ViewState["StepNumber"];
                return (o == null) ? 0 : (decimal)o;
            }
            set { ViewState["StepNumber"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsAlphanumeric
        {
            get
            {
                object o = ViewState["IsAlphanumeric"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsAlphanumeric"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsDigitsOnly
        {
            get
            {
                object o = ViewState["IsDigitsOnly"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsDigitsOnly"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsPrice
        {
            get
            {
                object o = ViewState["IsPrice"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsPrice"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsLettersWithBasicPunc
        {
            get
            {
                object o = ViewState["IsLettersWithBasicPunc"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsLettersWithBasicPunc"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsLettersOnly
        {
            get
            {
                object o = ViewState["IsLettersOnly"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsLettersOnly"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(false),
        ]
        public bool IsNoWhiteSpace
        {
            get
            {
                object o = ViewState["IsNoWhiteSpace"];
                return (o == null) ? false : (bool)o;
            }
            set { ViewState["IsNoWhiteSpace"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        ]
        public int MinLength
        {
            get
            {
                object o = ViewState["MinLength"];
                return (o == null) ? 0 : (int)o;
            }
            set { ViewState["MinLength"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        ]
        public int MinWords
        {
            get
            {
                object o = ViewState["MinWords"];
                return (o == null) ? 0 : (int)o;
            }
            set { ViewState["MinWords"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        ]
        public int MaxWords
        {
            get
            {
                object o = ViewState["MaxWords"];
                return (o == null) ? 0 : (int)o;
            }
            set { ViewState["MaxWords"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        ]
        public string EqualTo
        {
            get
            {
                string t = (string)ViewState["EqualTo"];
                return t ?? String.Empty;
            }
            set { ViewState["EqualTo"] = value; }
        }
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(true),
        ]
        public bool EnableClientValidation
        {
            get
            {
                object o = ViewState["EnableClientValidation"];
                return (o == null) ? true : (bool)o;
            }
            set { ViewState["EnableClientValidation"] = value; }
        }
        public bool IsValid
        {
            get
            {
                object o = ViewState["IsValid"];
                if (o == null)
                {
                    // all the regex are the same of client validation from Web Experience Toolkit (WET)
                    if (!Visible) return true;
                    if (IsRequired && string.IsNullOrEmpty(Text)) return false;
                    if (!IsRequired && string.IsNullOrEmpty(Text)) return true;
                    if (IsPhoneNumber)
                    {
                        string regExp = @"^[+]?[0-9]{0,1}[-. ]?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsPostalCode)
                    {
                        string regExp = @"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ]( )?\d[ABCEGHJKLMNPRSTVWXYZ]\d$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsEmail)
                    {
                        string regExp = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsGovernmentEmail)
                    {
                        string regExp = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@((?:[a-zA-Z]([a-zA-Z0-9-.]{0,61}[a-zA-Z0-9]).gc{1})|canada|scc-csc)(.ca){1}$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsUrl)
                    {
                        string regExp = @"^(?:(?:(?:https?|ftp):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})).?)(?::\d{2,5})?(?:[/?#]\S*)?$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsDate)
                    {
                        string regExp = @"^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                        if (!string.IsNullOrEmpty(MinDate))
                        {
                            DateTime.TryParse(Text, out DateTime date);
                            DateTime.TryParse(MinDate, out DateTime minDate);
                            if (date < minDate) return false;
                        }
                        if (!string.IsNullOrEmpty(MaxDate))
                        {
                            DateTime.TryParse(Text, out DateTime date);
                            DateTime.TryParse(MaxDate, out DateTime maxDate);
                            if (date > maxDate) return false;
                        }
                    }
                    if (IsTime)
                    {
                        string regExp = @"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsNumber)
                    {
                        string regExp = @"^(?:-?\d+|-?\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (MinNumber != 0 || MaxNumber != 0)
                    {
                        Decimal.TryParse(Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out Decimal i);
                        if (MinNumber != 0 && i < MinNumber) return false;
                        if (MaxNumber != 0 && i > MaxNumber) return false;
                    }
                    if (StepNumber != 0)
                    {
                        Decimal.TryParse(Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out Decimal i);
                        if (i != 0 && (i % StepNumber) != 0) return false;
                    }
                    if (IsAlphanumeric)
                    {
                        string regExp = @"^[a-zA-Z0-9_]*$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsDigitsOnly)
                    {
                        string regExp = @"^\d+$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsPrice)
                    {
                        string regExp = @"^[0-9]+([\,|\.]{0,1}[0-9]{2}){0,1}$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsLettersWithBasicPunc)
                    {
                        string regExp = @"^[A-Za-z-.,()'""]*$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsLettersOnly)
                    {
                        string regExp = @"^[a-zA-Z]*$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(Text, regExp)) return false;
                    }
                    if (IsNoWhiteSpace)
                    {
                        if (string.IsNullOrWhiteSpace(Text)) return false;
                    }
                    if (MinLength > 0)
                    {
                        if (Text.Length < MinLength) return false;
                    }
                    if (MaxLength > 0)
                    {
                        if (Text.Length > MaxLength) return false;
                    }
                    if (MinWords != 0 || MaxWords != 0)
                    {
                        if (MinWords < 0 || MaxWords < 0) return false;

                        char[] delimiters = new char[] { ' ', '\r', '\n' };
                        int i = Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

                        if (MinWords != 0 && i < MinWords) return false;
                        if (MaxWords != 0 && i > MaxWords) return false;
                    }
                    if (!string.IsNullOrEmpty(EqualTo))
                    {
                        Control ctrl = Page.FindControlRecursive(EqualTo.TrimStart('#'));
                        if (ctrl == null) return false;
                        else if (!(ctrl is TextBox)) return false;
                        else if (ctrl is TextBox && !((TextBox)ctrl).Text.Equals(this.Text)) return false;
                    }
                    return true;
                }
                else
                {
                    return (bool)o;
                }
            }
            set { ViewState["IsValid"] = value; }
        }

        private void VerifyValidationConflicts()
        {
            if (IsDate && !string.IsNullOrEmpty(MinDate) && MinNumber != 0)
                throw new ArgumentException("You van not add validation for minNumber with minDate.");

            if (IsDate && !string.IsNullOrEmpty(MaxDate) && MaxNumber != 0)
                throw new ArgumentException("You van not add validation for maxNumber with maxDate.");

            if (IsDate && !string.IsNullOrEmpty(MinDate) && !string.IsNullOrEmpty(MaxDate))
            {
                DateTime.TryParse(MinDate, out DateTime minDate);
                DateTime.TryParse(MaxDate, out DateTime maxDate);
                if (minDate >= maxDate)
                    throw new Exception("Date conflict between MinDate(" + MinDate + ")  and MaxDate(" + MaxDate + ")");
            }

            if (MinNumber != 0 && MaxNumber != 0 && MinNumber >= MaxNumber)
                throw new Exception("Number conflict between MinNumber(" + MinNumber + ")  and MaxNumber(" + MaxNumber + ")");

            if (MinLength != 0 && MaxLength != 0 && MinLength >= MaxLength)
                throw new Exception("Length conflict between MinLength(" + MinLength + ")  and MaxLength(" + MaxLength + ")");

            if (MinWords != 0 && MaxWords != 0 && MinWords >= MaxWords)
                throw new Exception("Words conflict between MinWords(" + MinWords + ")  and MaxWords(" + MaxWords + ")");
        }

        protected override void OnLoad(EventArgs e)
        {
            // startup init script
            WetControls.Extensions.ClientScript.InitScript(Page);

            // datepicker script
            if (IsDate)
            {
                WetControls.Extensions.ClientScript.InitDatePicker(this.Page);
            }
            // gouvernment email script
            if (IsGovernmentEmail)
            {
                WetControls.Extensions.ClientScript.InitFrmvldGovemail(this.Page, this.ErrorGovEmail);
            }
            // price script
            if (IsPrice)
            {
                WetControls.Extensions.ClientScript.InitFrmvldPrice(this.Page, this.ErrorPrice);
            }

            if (EnableClientValidation)
            {
                // attributes validation conflicts
                VerifyValidationConflicts();

                if (IsRequired)
                {
                    base.Attributes.Add("required", "required");
                }
                if (IsPhoneNumber)
                {
                    base.Attributes.Add("data-rule-phoneUS", "true");
                }
                if (IsPostalCode)
                {
                    base.Attributes.Add("size", "7");
                    base.Attributes.Add("data-rule-postalCodeCA", "true");
                }
                if (IsEmail)
                {
                    base.Attributes.Add("type", "email");
                }
                if (IsGovernmentEmail)
                {
                    base.Attributes.Add("data-rule-govemail", "true");
                }
                if (IsUrl)
                {
                    base.Attributes.Add("type", "url");
                }
                if (IsDate)
                {
                    base.Attributes.Add("type", "date");
                    base.Attributes.Add("data-rule-dateISO", "true");
                }
                if (IsTime)
                {
                    base.Attributes.Add("type", "time");
                }
                if (IsAlphanumeric)
                {
                    base.Attributes.Add("pattern", "[A-Za-z0-9_\\s]");
                    base.Attributes.Add("data-rule-alphanumeric", "true");
                }
                if (IsDigitsOnly)
                {
                    base.Attributes.Add("type", "number");
                    base.Attributes.Add("data-rule-digits", "true");
                }
                if (IsPrice)
                {
                    base.Attributes.Add("data-rule-price", "true");
                }
                if (IsLettersOnly)
                {
                    base.Attributes.Add("pattern", "[A-Za-z\\s]");
                    base.Attributes.Add("data-rule-lettersonly", "true");
                }
                if (IsLettersWithBasicPunc)
                {
                    base.Attributes.Add("pattern", "[A-Za-z-.,()'\"\\s]");
                    base.Attributes.Add("data-rule-letterswithbasicpunc", "true");
                }
                if (IsNoWhiteSpace)
                {
                    base.Attributes.Add("pattern", "[A-Za-z-.,()'\"\\s]");
                    base.Attributes.Add("data-rule-nowhitespace", "true");
                }
                if (IsNumber)
                {
                    base.Attributes.Add("type", "number");
                }
                if (MinNumber != 0 && MaxNumber != 0)
                {
                    base.Attributes.Add("data-rule-range", string.Format("[{0},{1}]", MinNumber, MaxNumber));
                }
                else if (MinNumber != 0)
                {
                    base.Attributes.Add("min", MinNumber.ToString());
                }
                else if (MaxNumber != 0)
                {
                    base.Attributes.Add("max", MaxNumber.ToString());
                }
                if (StepNumber != 0)
                {
                    base.Attributes.Add("step", StepNumber.ToString());
                }
                if (MinLength > 0 && MaxLength > 0)
                {
                    base.Attributes.Add("data-rule-rangelength", string.Format("[{0},{1}]", MinLength, MaxLength));
                }
                else if (MinLength > 0)
                {
                    base.Attributes.Add("data-rule-minlength", MinLength.ToString());
                }
                else if (MaxLength > 0)
                {
                    base.Attributes.Add("maxlength", MaxLength.ToString());
                }
                if (MinWords > 0 && MaxWords > 0)
                {
                    base.Attributes.Add("data-rule-rangeWords", string.Format("[{0},{1}]", MinWords, MaxWords));
                }
                else if (MinWords > 0)
                {
                    base.Attributes.Add("data-rule-minWords", MinWords.ToString());
                }
                else if (MaxWords > 0)
                {
                    base.Attributes.Add("data-rule-maxWords", MaxWords.ToString());
                }
                if (!string.IsNullOrEmpty(EqualTo))
                {
                    Control ctrl = Page.FindControlRecursive(EqualTo.TrimStart('#')); // prevent tag at beginning
                    base.Attributes.Add("data-rule-equalTo", (ctrl == null ? "#" + EqualTo : "#" + ctrl.ClientID));
                }
                if (!string.IsNullOrEmpty(ValidationErrorMsg))
                {
                    base.Attributes.Add("data-msg", ValidationErrorMsg);
                }
            }
            if (!string.IsNullOrEmpty(Placeholder))
            {
                base.Attributes.Add("placeholder", Placeholder);
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.AddCssClass("form-control");

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page.AutoPostBackControl == this)
            {
                // validate after postback
                WetControls.Extensions.ClientScript.ValidateScript(Page);
            }
            if (GroupSize == ENUM_GROUP_SIZE.Small) writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-group form-group-sm", false);
            else if (GroupSize == ENUM_GROUP_SIZE.Large) writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-group form-group-lg", false);
            else writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-group", false);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID, false);
            if (IsRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "required", false);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            if (string.IsNullOrEmpty(LabelCssClass))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "field-name", false);
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "field-name " + LabelCssClass, false);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(LabelText);
            writer.RenderEndTag();
            if (IsRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "required", false);
                writer.RenderBeginTag(HtmlTextWriterTag.Strong);
                writer.Write(RequiredText);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            // call the base class's Render method
            base.Render(writer);

            writer.RenderEndTag();
        }

        public void Clear()
        {
            this.Text = null;
            // reset validation
            ViewState["IsValid"] = null;
        }
    }
}