using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.ViewModels
{
    public class DESViewModel
    {
        public string Text { get; set; }
        public string Key { get; set; }
        public string TextFormat { get; set; }
        public string KeyFormat { get; set; }
        public string DESResult { get; set; }

    }
}