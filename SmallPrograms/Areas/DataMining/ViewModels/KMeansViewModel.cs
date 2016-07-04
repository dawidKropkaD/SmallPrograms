using SmallPrograms.Areas.DataMining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.ViewModels
{
    public class KMeansViewModel
    {
        List<Point> pointList = new List<Point>();
        List<Centroid> centroidList = new List<Centroid>();
    }
}