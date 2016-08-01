using SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.Models;
using SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.Controllers
{
    public class ProgramsController : Controller
    {
        // GET: AdvancedMethodsOfInformationProtection/Programs
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DES()
        {
            return View(new DESViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DES(DESViewModel desVM, string btnSubmit)
        {
            if (ModelState.IsValid == false)
            {
                return View(desVM);
            }

            DESBusinessLayer desBL = new DESBusinessLayer();
            List<byte[]> binaryTekst64BitsList = new List<byte[]>();    //binary text divided on list, which every item contains 64-bit 
            string sBinaryTekst = string.Empty;
            string sBinaryKey = string.Empty;
            byte[] binaryTekst;
            byte[] binaryKey = new byte[64];

            desVM.Text = desVM.Text.Replace(" ", string.Empty);
            desVM.Key = desVM.Key.Replace(" ", string.Empty);

            #region  Convert text to byte array
            if (desVM.TextFormat == "hex")
            {
                if (desBL.IsValidHex(desVM.Text))
                {
                    sBinaryTekst= String.Join(String.Empty, desVM.Text.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                    binaryTekst = desBL.ConvertStringToArray(sBinaryTekst);
                }
                else
                {
                    ModelState.AddModelError("", "Tekst nie jest liczbą szestastkową");
                    return View(desVM);
                }
            }
            else //if(desVM.TextFormat == "binary")
            {
                if (desBL.IsValidBinary(desVM.Text))
                {
                    binaryTekst = desBL.ConvertStringToArray(desVM.Text);
                }
                else
                {
                    ModelState.AddModelError("", "Tekst nie jest liczbą dwójkową");
                    return View(desVM);
                }
            }
            #endregion

            #region Convert key to byte array
            if (desVM.KeyFormat == "hex")
            {
                if (desVM.Key.Length > 16)
                {
                    ModelState.AddModelError("", "Klucz nie może być dłuższy niż 16 cyfr zapisanych w systemie szesnastkowym");
                    return View(desVM);
                }

                if (desBL.IsValidHex(desVM.Key))
                {
                    desVM.Key = desVM.Key.PadLeft(16, '0');
                    sBinaryKey = String.Join(String.Empty, desVM.Key.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                    binaryKey = desBL.ConvertStringToArray(sBinaryKey);
                }
                else
                {
                    ModelState.AddModelError("", "Klucz nie jest liczbą szestastkową");
                    return View(desVM);
                }
            }
            else if (desVM.KeyFormat == "binary")
            {
                if (desVM.Key.Length > 64)
                {
                    ModelState.AddModelError("", "Klucz nie może być dłuższy niż 64 cyfry zapisane w systemie dwójkowym");
                    return View(desVM);
                }

                if (desBL.IsValidBinary(desVM.Key))
                {
                    desVM.Key = desVM.Key.PadLeft(64, '0');
                    binaryKey = desBL.ConvertStringToArray(desVM.Key);
                }
                else
                {
                    ModelState.AddModelError("", "Klucz nie jest liczbą dwójkową");
                    return View(desVM);
                }
            }
            #endregion

            if (btnSubmit == "Szyfruj")
            {
                desVM.DoEncrypt = true;
            }
            if (btnSubmit == "Deszyfruj")
            {
                desVM.DoEncrypt = false;
            }

            binaryTekst64BitsList = desBL.ArrayShareOnListOf64ElementsArrays(binaryTekst);

            foreach (var item in binaryTekst64BitsList)
            {
                byte[] binaryDESResult = desBL.DESAlgorithm(binaryKey, item, desVM.DoEncrypt);
                string sBinaryDESResult = desBL.ConvertArrayToString(binaryDESResult);

                desVM.DESResultBinary += sBinaryDESResult;
                desVM.DESResultHex += (Convert.ToInt64(sBinaryDESResult, 2).ToString("X")).PadLeft(16, '0');
            }

            desVM.DESResultBinary = desBL.AppendAtPosition(desVM.DESResultBinary, 4, " ");
            desVM.DESResultHex = desBL.AppendAtPosition(desVM.DESResultHex, 4, " ");
            desVM.DisplayDESResult = "inline";

            return View(desVM);
        }
    }
}