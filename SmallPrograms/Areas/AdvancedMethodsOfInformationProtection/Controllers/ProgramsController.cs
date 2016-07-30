using SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.Models;
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
            DESBusinessLayer des = new DESBusinessLayer();
            byte[] encryptedData = des.DESAlgorithm(new byte[64] { 1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,0,0,0,0,1,0,0,1,0,0,0,1,1,0,0,0,0,0,1,0,0,1,1,1,0,0,1,1,0,1,1,0,1,1,0,0,1,1,0,0,1,1,0,1,1,1,0,1 },
                new byte[64] { 1,1,0,0,0,0,0,0,1,0,1,1,0,1,1,1,1,0,1,0,1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,1,1,1,1,1,0,0,1,1,1,0,1,0,1,0,0,0,0,0,1,0,1,0,0,1,1,1,0,0 }, true);

            return View(encryptedData);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DES(string key, string text)
        {
            return View();
        }

        #endregion
    }
}