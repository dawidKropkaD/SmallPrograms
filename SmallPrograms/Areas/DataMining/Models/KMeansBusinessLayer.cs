using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class KMeansBusinessLayer
    {
        /// <summary>
        /// Set values to false in dictionary
        /// </summary>
        /// <param name="dict">Dictionary</param>
        /// <returns>Dictionary with all false values</returns>
        public Dictionary<string, bool> SetValuesToFalseInDict(Dictionary<string, bool> dict)
        {
            List<string> keys = new List<string>(dict.Keys);
            foreach (var key in keys)
            {
                dict[key] = false;
            }
            return dict;
        }

        /// <summary>
        /// Check if number of points, number of centroids and dimension are valid
        /// </summary>
        /// <param name="pointsNumber">Points number</param>
        /// <param name="centroidsNumber">Centroids number</param>
        /// <param name="dimnesion">Dimension</param>
        /// <returns>Error message if data are not valid, otherwise empty string</returns>
        public string InputDataAreValid(int pointsNumber, int centroidsNumber, int dimnesion)
        {
            if (pointsNumber < 2)
            {
                return "Liczba punktów musi być większa niż 1";
            }

            if (centroidsNumber < 1)
            {
                return "Liczba centroidów musi być większa niż 0";
            }

            if (dimnesion < 1)
            {
                return "Wymiar musi być większy niż 0";
            }

            return string.Empty;
        }

        /// <summary>
        /// Check if points and centroids have the same dimnesion
        /// </summary>
        /// <param name="pList">List of points</param>
        /// <param name="cList">List of centroids</param>
        /// <returns>Error message if data are not valid, otherwise empty string</returns>
        public string InputDataAreValid(List<Point> pList, List<Centroid> cList)
        {
            int dimension = pList[0].Coordinate.Length;

            for(int i = 0; i < pList.Count; i++)
            {
                if (pList[i].Coordinate.Length != dimension)
                {
                    return "Nie wszystkie punkty i centroidy mają ten sam wymiar";
                }
            }

            for (int i = 0; i < cList.Count; i++)
            {
                if (cList[i].Coordinate.Length != dimension)
                {
                    return "Nie wszystkie punkty i centroidy mają ten sam wymiar";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Random values for points and centroids 
        /// </summary>
        /// <param name="pointsNumber">points number</param>
        /// <param name="centroidsNumber">centroids number</param>
        /// <param name="dimension">dimension for points and centroids</param>
        /// <returns>Random values</returns>
        public PointsAndCentroids RandomInputData(int pointsNumber, int centroidsNumber, int dimension)
        {
            Coordinates c = new Coordinates();
            PointsAndCentroids pointsAndCentroids = new PointsAndCentroids();
            pointsAndCentroids.PointList = new List<Point>();
            pointsAndCentroids.CentroidList = new List<Centroid>();
            
            for (int i = 0; i < pointsNumber; i++)
            {
                double[] coordinate = c.RandomCoordinates(dimension, -100, 101);
                pointsAndCentroids.PointList.Add(new Point(0, coordinate));
            }

            for (int i = 0; i < centroidsNumber; i++)
            {
                double[] coordinate = c.RandomCoordinates(dimension, -100, 101);
                pointsAndCentroids.CentroidList.Add(new Centroid(i + 1, coordinate));
            }

            return pointsAndCentroids;
        }
    }
}