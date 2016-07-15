using SmallPrograms.Areas.DataMining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmallPrograms.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Temp()
        {
            PerceptronViewModel vm = new PerceptronViewModel();

            vm.WeightVector = new double?[2];
            vm.WeightVector[0] = 3;
            vm.WeightVector[1] = 9;
            vm.NewThreshold = 99;

            return View();
        }

        [HttpPost]
        public ActionResult Temp(int IterationNumberInDeltaRule)
        {
            PerceptronViewModel vm = new PerceptronViewModel();
            vm.IterationNumberInDeltaRule = 9;

            return View(vm);
        }
    }
}