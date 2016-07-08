using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class Coordinates
    {
        public double[] Coordinate { get; set; }

        Random r = new Random();

        public double[] RandomCoordinates(int dimension, int minValue, int maxValue)
        {
            Coordinate = new double[dimension];
            for(int i = 0; i < dimension; i++)
            {
                Coordinate[i] = r.Next(minValue, maxValue);
            }

            return Coordinate;
        }
    }
}