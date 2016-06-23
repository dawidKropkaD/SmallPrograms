using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmallPrograms.Controllers
{
    public class DataMiningController : Controller
    {
        // GET: DataMining
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Perceptron()
        {
            return View();
        }

        public ActionResult KMeans()
        {
            int zmiennaDoUsuniecia = 0;
            zmiennaDoUsuniecia++;

            return View();
        }
    }
}