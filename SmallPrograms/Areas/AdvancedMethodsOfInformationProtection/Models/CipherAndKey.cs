using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.Models
{
    /// <summary>
    /// Cipher and key for monoalphabetic cipher
    /// </summary>
    public class CipherAndKey
    {
        [Display(Name = "Szyfr")]
        public string Cipher { get; set; }

        [Display(Name = "Klucz")]
        public Dictionary<char, char> Key { get; set; }


        public CipherAndKey()
        {
            Key = new Dictionary<char, char>();
        }
    }
}