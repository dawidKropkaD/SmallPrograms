using SmallPrograms.Areas.DataMining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.ViewModels
{
    public class KMeansViewModel
    {
        public List<Point> pointList { get; set; }
        public List<Centroid> centroidList { get; set; }
    }
}