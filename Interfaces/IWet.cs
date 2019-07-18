namespace WetControls.Interfaces
{
    public interface IWet
    {
        string ID { get; set; }
        bool Visible { get; set; }
        string CssClass { get; set; }
        string LabelText { get; set; }
        string LabelCssClass { get; set; }
        string ValidationErrorMsg { get; set; }
        bool IsRequired { get; set; }
        bool IsValid { get; set; }
        bool EnableClientValidation { get; set; }
    }
}
