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

        public Point() { }

        public Point(int group, double[] coordinate)
        {
            this.Group = group;
            this.Coordinate = coordinate;
        }

        public Point DeepCopy(Point p)
        {
            Point copy = new Point();
            copy.Group = p.Group;
            copy.Coordinate = new double[p.Coordinate.Length];

            for(int i = 0; i < p.Coordinate.Length; i++)
            {
                copy.Coordinate[i] = p.Coordinate[i];
            }

            return copy;
        }

        public List<Point> DeepCopy(List<Point> pList)
        {
            List<Point> copyList = new List<Point>();
            for(int i = 0; i < pList.Count; i++)
            {
                copyList.Add(DeepCopy(pList[i]));
            }

            return copyList;
        }
    }
}