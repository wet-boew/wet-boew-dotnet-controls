using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WetControls.Interfaces
{
    public interface IWet
    {
        string LabelText { get; set; }
        string LabelCssClass { get; set; }
        string ValidationErrorMsg { get; set; }
        bool IsRequired { get; set; }
        bool IsValid { get; set; }
        bool EnableClientValidation { get; set; }
    }
}
