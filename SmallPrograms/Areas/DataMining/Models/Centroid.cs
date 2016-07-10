using System;
using System.Collections;
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

        public Centroid DeepCopy(Centroid obj)
        {
            Centroid copy = new Centroid();
            copy.Coordinate = new double[obj.Coordinate.Length];

            copy.GroupNumber = obj.GroupNumber;
            for(int i = 0; i < obj.Coordinate.Length; i++)
            {
                copy.Coordinate[i] = obj.Coordinate[i];
            }

            return copy;
        }

        public List<Centroid> DeepCopy(List<Centroid> objList)
        {
            List<Centroid> copyList = new List<Centroid>();
            
            for(int j = 0; j < objList.Count; j++)
            {
                copyList.Add(DeepCopy(objList[j]));
            }

            return copyList;
        }
    }

    class CentroidComparer : IEqualityComparer<Centroid>
    {
        public bool Equals(Centroid c1, Centroid c2)
        {
            if (c1.GroupNumber != c2.GroupNumber)
            {
                return false;
            }
            for (int i = 0; i < c1.Coordinate.Length; i++)
            {
                if (c1.Coordinate[i] != c2.Coordinate[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(Centroid obj)
        {
            return obj.GroupNumber.GetHashCode();
        }
    }
}