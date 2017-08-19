using SmallPrograms.Areas.DataMining.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.ViewModels
{
    public class KMeansViewModel
    {
        /// <summary>
        /// used in the case of manual data entry
        /// </summary>
        public List<Point> PointList { get; set; }

        /// <summary>
        /// used in the case of manual data entry
        /// </summary>
        public List<Centroid> CentroidList { get; set; }

        /// <summary>
        /// used in the case of random data entry
        /// </summary>
        [Display(Name ="Liczba punktów: ")]
        public int PointsNumber { get; set; }

        /// <summary>
        /// used in the case of random data entry
        /// </summary>
        [Display(Name = "Liczba centroidów: ")]
        public int CentroidsNumber { get; set; }

        /// <summary>
        /// used in the case of random data entry
        /// </summary>
        [Display(Name = "Wymiar: ")]
        public int Dimnesion { get; set; }

        public Dictionary<string, bool> DataEntryMethods { get; set; }

        public string SelectedMethod { get; set; }



        public KMeansViewModel()
        {
            DataEntryMethods = new Dictionary<string, bool>();
            DataEntryMethods.Add("manually", true);
            DataEntryMethods.Add("random", false);
        }

        public KMeansViewModel(int pointsNumber, int centroidsNumber, int dimension)
        {
            DataEntryMethods = new Dictionary<string, bool>();
            DataEntryMethods.Add("manually", true);
            DataEntryMethods.Add("random", false);

            PointList = new List<Point>();
            CentroidList = new List<Centroid>();

            for (int i = 0; i < pointsNumber; i++)
            {
                PointList.Add(new Point(-1, new double[dimension]));
            }

            for (int i = 0; i < centroidsNumber; i++)
            {
                CentroidList.Add(new Centroid(i + 1, new double[2]));
            }
        }
    }
}