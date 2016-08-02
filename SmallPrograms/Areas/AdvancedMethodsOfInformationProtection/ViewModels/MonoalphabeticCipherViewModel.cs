using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.ViewModels
{
    public class MonoalphabeticCipherViewModel : Models.CipherAndKey
    {
        private string displayResult = "none";

        [Required]
        [Display(Name = "Tekst jawny")]
        public string PlainText { get; set; }

        [Required]
        [Display(Name = "Wzorzec")]
        public string PatternText { get; set; }

        /// <summary>
        /// Value corresponding to CSS display property, in result part in view.
        /// </summary>
        public string DisplayResult
        {
            get
            {
                return displayResult;
            }
            set
            {
                displayResult = value;
            }
        }
    }
}