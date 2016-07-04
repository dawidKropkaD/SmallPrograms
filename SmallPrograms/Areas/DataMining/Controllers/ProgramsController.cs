using SmallPrograms.Areas.DataMining.Models;
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
            List<Point> pList = new List<Point>();
            Point p = new Point();
            Point p2 = new Point();
            Point p3 = new Point();

            p.Coordinate = new double[3];
            pList.Add(p);
            pList.Add(p);

            p3.Coordinate = new double[3];
            p3.Coordinate[0] = 10;
            p3.Coordinate[1] = 11;
            p3.Coordinate[2] = 12;
            pList.Add(p3);

            p2.Coordinate = new double[2];
            p2.Coordinate[0] = 3;
            p2.Coordinate[1] = 6;
            pList.Add(p2);
            return View(pList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KMeans(string value, List<Point> pointList, string Str)
        {
            pointList[3].Coordinate[0] = 7;
            pointList[0].Coordinate[0] = 7;
            if (ModelState.IsValid)
            {
                ViewBag.Valid = "True";
            }
            else
            {
                ViewBag.Valid = "False";
            }

            return View(pointList);
        }
    }
}