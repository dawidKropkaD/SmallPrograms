using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class Centroid : Coordinates
    {
        public int GroupNumber { get; set; }

        public Centroid()
        {
        }

        public Centroid(int groupNumber, double[] coordinate)
        {
            this.GroupNumber = groupNumber;
            this.Coordinate = coordinate;
        }
    }
}