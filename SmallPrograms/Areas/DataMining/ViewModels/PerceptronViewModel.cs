using SmallPrograms.Areas.DataMining.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.ViewModels
{
    public class PerceptronViewModel
    {
        [Display(Name ="Wektor wag: ")]
        public double[] WeightVector { get; set; }

        [Display(Name ="Stała ucząca: ")]
        [Range(0.01, 0.9)]
        public double LearningRate { get; set; }

        [Display(Name ="Próg: ")]
        public double Threshold { get; set; }   //próg

        [Display(Name = "Nowy próg: ")]
        public double NewThreshold { get; set; }

        [Display(Name = "Wynik: ")]
        public string Output { get; set; }

        [Display(Name = "Liczebność zbioru treningowego: ")]
        [Range(1, double.MaxValue)]
        public int NumberOfExamplesInTrainingSet { get; set; }

        [Display(Name = "Liczba iteracji w regule delta: ")]
        public int IterationNumberInDeltaRule { get; set; }

        [Display(Name = "Maksymalna liczba iteracji w regule delta: ")]
        [Range(1, double.MaxValue)]
        public int MaxIterationNumberInDeltaRule { get; set; }

        [Display(Name ="Współrzędna X: ")]
        public double XCoordinate { get; set; }

        [Display(Name ="Współrzędna Y: ")]
        public double YCoordinate { get; set; }

        public bool DisplayResult { get; set; }
    }
}