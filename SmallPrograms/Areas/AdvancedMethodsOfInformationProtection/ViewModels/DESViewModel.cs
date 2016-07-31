using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.ViewModels
{
    public class DESViewModel
    {
        private string displayDESResult = "none";

        [Required]
        [Display(Name = "Tekst")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Klucz")]
        public string Key { get; set; }
        public string TextFormat { get; set; }
        public string KeyFormat { get; set; }

        /// <summary>
        /// True for encrypt, false for decrypt
        /// </summary>
        public bool DoEncrypt { get; set; }

        [Display(Name = "Wynik algorytmu DES w postaci szesnastkowej")]
        public string DESResultHex { get; set; }

        [Display(Name = "Wynik algorytmu DES w postaci binarnej")]
        public string DESResultBinary { get; set; }

        /// <summary>
        /// Value corresponding to CSS display property, in DES result part in view.
        /// </summary>
        public string DisplayDESResult
        {
            get
            {
                return displayDESResult;
            }
            set
            {
                displayDESResult = value;
            }
        }

    }
}