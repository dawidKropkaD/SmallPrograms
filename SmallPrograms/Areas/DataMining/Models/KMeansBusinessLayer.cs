using SmallPrograms.Areas.DataMining.ViewModels;
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

        public KMeansResultViewModel KMeansAlgorithm(List<Point> pList, List<Centroid> cList)
        {
            List<Centroid> previousCentroidList = new List<Centroid>();
            Centroid c = new Centroid();
            Point p = new Point();
            KMeansResultViewModel result = new KMeansResultViewModel();
            
            result.InputPointList = p.DeepCopy(pList);
            result.InputCentroidList = c.DeepCopy(cList);
            result.NumberOfIterations = 0;

            do
            {
                result.NumberOfIterations++;
                previousCentroidList = c.DeepCopy(cList);
                SetGroups(cList, pList);
                MoveCentroids(pList, cList);
            } while (!previousCentroidList.SequenceEqual(cList, new CentroidComparer()));

            result.OutputPointList = pList;
            result.OutputCentroidList = cList;

            return result;
        }

        public void MoveCentroids(List<Point> pList, List<Centroid> cList)
        {
            for (int i = 0; i < cList.Count; i++)
            {
                var points = pList.Select(p => p).Where(g => g.Group == cList[i].GroupNumber);
                if (points.Count() != 0)
                {
                    for(int j = 0; j < cList[i].Coordinate.Length; j++)
                    {
                        double sum = points.Select(p => p.Coordinate[j]).Sum();
                        double count = points.Select(p => p.Coordinate[j]).Count();
                        cList[i].Coordinate[j] = sum / count;
                    }
                }
            }
        }

        public void SetGroups(List<Centroid> cList, List<Point> pList)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                double[] distanceMemory = new double[cList.Count];
                for (int j = 0; j < cList.Count; j++)
                {
                    distanceMemory[j] = DistanceBetweenPoints(pList[i].Coordinate, cList[j].Coordinate);
                }
                int indexOfNearestCentroid = GetIndexLeastElementInArray(distanceMemory);
                pList[i].Group = cList[indexOfNearestCentroid].GroupNumber;
            }
        }

        public double DistanceBetweenPoints(double[] p1, double[] p2)
        {
            double distance = 0;
            for(int k = 0; k < p1.Length; k++)
            {
                distance += Math.Pow(p2[k] - p1[k], 2);
            }
            distance = Math.Sqrt(distance);

            return distance;
        }

        public int GetIndexLeastElementInArray(double[] array)
        {
            double min = array[0];
            int index = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (min > array[i])
                {
                    min = array[i];
                    index = i;
                }
            }

            return index;
        }
    }
}