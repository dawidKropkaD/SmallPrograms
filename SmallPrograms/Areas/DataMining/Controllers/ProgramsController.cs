using SmallPrograms.Areas.DataMining.Models;
using SmallPrograms.Areas.DataMining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmallPrograms.Areas.DataMining.Controllers
{
    public class ProgramsController : Controller
    {
        // GET: DataMining/Programs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KMeans()
        {
            KMeansViewModel kMeansVM = new KMeansViewModel();
            kMeansVM.pointList = new List<Point>();
            kMeansVM.centroidList = new List<Centroid>();

            //add 3 2D points
            kMeansVM.pointList.Add(new Point(-1, new double[2]));
            kMeansVM.pointList.Add(new Point(-1, new double[2]));
            kMeansVM.pointList.Add(new Point(-1, new double[2]));

            //add 2 centroids
            kMeansVM.centroidList.Add(new Centroid(1, new double[2]));
            kMeansVM.centroidList.Add(new Centroid(2, new double[2]));


            return View(kMeansVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KMeans(KMeansViewModel kMeansVM)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Valid = "True";
            }
            else
            {
                ViewBag.Valid = "False";
            }

            return View(kMeansVM);
        }
    }
}