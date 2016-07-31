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

        #region DES

        public ActionResult DES()
        {
            //DESBusinessLayer des = new DESBusinessLayer();
            //byte[] encryptedData = des.DESAlgorithm(new byte[64] { 1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,0,0,0,0,1,0,0,1,0,0,0,1,1,0,0,0,0,0,1,0,0,1,1,1,0,0,1,1,0,1,1,0,1,1,0,0,1,1,0,0,1,1,0,1,1,1,0,1 },
            //    new byte[64] { 1,1,0,0,0,0,0,0,1,0,1,1,0,1,1,1,1,0,1,0,1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,1,1,1,1,1,0,0,1,1,1,0,1,0,1,0,0,0,0,0,1,0,1,0,0,1,1,1,0,0 }, false);

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DES(DESViewModel desVM)
        {
            DESBusinessLayer desBL = new DESBusinessLayer();
            List<byte[]> binaryTekst64BitsList = new List<byte[]>();
            string sBinaryTekst = string.Empty;
            byte[] binaryTekst;
            byte[] binaryKey = new byte[64];

            desVM.Text = desVM.Text.Replace(" ", string.Empty);
            desVM.Key = desVM.Key.Replace(" ", string.Empty);

            if (desVM.TextFormat == "hex")
            {
                if (desBL.IsValidHex(desVM.Text))
                {
                    sBinaryTekst= String.Join(String.Empty, desVM.Text.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                }
                else
                {
                    ModelState.AddModelError("", "Tekst nie jest liczbą szestastkową");
                }
            }
            else if (desVM.TextFormat == "decimal")
            {

            }
            else if (desVM.TextFormat == "binary")
            {

            }

            return View();
        }

        #endregion
    }
}