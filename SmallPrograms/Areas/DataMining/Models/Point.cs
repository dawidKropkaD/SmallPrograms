using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class Point : Coordinates
    {
        public int Group { get; set; }

        public Point()
        {
        }

        public Point(int group, double[] coordinate)
        {
            this.Group = group;
            this.Coordinate = coordinate;
        }
    }
}