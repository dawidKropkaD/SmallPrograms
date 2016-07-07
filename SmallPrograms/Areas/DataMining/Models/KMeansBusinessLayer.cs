using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class KMeansBusinessLayer
    {
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
        /// Check if input data are valid for random method
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

        public void RandomInputData(int pointsNumber, int centroidsNumber, int dimnesion)
        {

        }
    }
}