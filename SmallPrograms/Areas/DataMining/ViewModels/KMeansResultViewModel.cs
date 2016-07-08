using SmallPrograms.Areas.DataMining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.ViewModels
{
    public class KMeansResultViewModel
    {
        public List<Point> inputPointList { get; set; }
        public List<Centroid> inputCentroidList { get; set; }
    }
}