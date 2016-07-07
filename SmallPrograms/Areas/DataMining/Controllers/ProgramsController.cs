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

            InitPointListForKMeansViewModel(kMeansVM);
            InitCentroidListForKMeansViewModel(kMeansVM);

            return View(kMeansVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KMeans(KMeansViewModel kMeansVM)
        {
            KMeansBusinessLayer kMeansBL = new KMeansBusinessLayer();

            //check validation input data
            if (kMeansVM.SelectedMethod == "random")
            {
                string errrorMessage = kMeansBL.InputDataAreValid(kMeansVM.PointsNumber, kMeansVM.CentroidsNumber, kMeansVM.Dimnesion);
                if (string.IsNullOrEmpty(errrorMessage) == false)
                {
                    ModelState.AddModelError("randomMethod", errrorMessage);
                }
                else
                {

                }
            }

            if (ModelState.IsValid)
            {
                TempData["pointList"] = kMeansVM.PointList;
                TempData["centroidList"] = kMeansVM.CentroidList;

                RedirectToAction("KMeansResult", "Programs");
            }
            else
            {
                kMeansVM.DataEntryMethods = kMeansBL.SetValuesToFalseInDict(kMeansVM.DataEntryMethods);
                kMeansVM.DataEntryMethods[kMeansVM.SelectedMethod] = true;

                bool pointListIsNotNull = kMeansVM.PointList != null;
                bool centroidListIsNotNull = kMeansVM.CentroidList != null;
                if (pointListIsNotNull == false)
                {
                    InitPointListForKMeansViewModel(kMeansVM);
                }
                if (centroidListIsNotNull == false)
                {
                    InitCentroidListForKMeansViewModel(kMeansVM);
                }
            }

            return View(kMeansVM);
        }

        public ActionResult KMeansResult()
        {
            List<Point> pList = new List<Point>();
            List<Centroid> cList = new List<Centroid>();

            pList = (List<Point>)TempData["pointList"];
            cList = (List<Centroid>)TempData["cList"];

            return View();
        }




        [NonAction]
        public void InitPointListForKMeansViewModel(KMeansViewModel kMeansVM)
        {
            kMeansVM.PointList = new List<Point>();

            //add 3 2D points
            kMeansVM.PointList.Add(new Point(-1, new double[2]));
            kMeansVM.PointList.Add(new Point(-1, new double[2]));
            kMeansVM.PointList.Add(new Point(-1, new double[2]));
        }

        [NonAction]
        public void InitCentroidListForKMeansViewModel(KMeansViewModel kMeansVM)
        {
            kMeansVM.CentroidList = new List<Centroid>();

            //add 2 2D centroids
            kMeansVM.CentroidList.Add(new Centroid(1, new double[2]));
            kMeansVM.CentroidList.Add(new Centroid(2, new double[2]));
        }
    }
}