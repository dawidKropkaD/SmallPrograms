using SmallPrograms.Areas.DataMining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.ViewModels
{
    public class KMeansResultViewModel
    {
        public List<Point> InputPointList { get; set; }
        public List<Centroid> InputCentroidList { get; set; }
        public List<Point> OutputPointList { get; set; }
        public List<Centroid> OutputCentroidList { get; set; }
        public int NumberOfIterations { get; set; }


        public KMeansResultViewModel()
        {
            InputPointList = new List<Point>();
            InputCentroidList = new List<Centroid>();
            OutputPointList = new List<Point>();
            OutputCentroidList = new List<Centroid>();
        }
    }
}