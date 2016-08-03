﻿using SmallPrograms.Areas.DataMining.Models;
using SmallPrograms.Areas.DataMining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmallPrograms.Areas.DataMining.Controllers
{
    public class ProgramsController : Controller
    {
        // GET: DataMining/Programs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Perceptron()
        {
            PerceptronViewModel perceptronVM = new PerceptronViewModel();
            perceptronVM.DisplayResult = false;
            perceptronVM.WeightVector = new double?[2];

            return View(perceptronVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perceptron(PerceptronViewModel perceptronVM, string btnSubmit)
        {
            //set visibility for result
            if (ModelState["NumberOfExamplesInTrainingSet"].Errors.Count != 0 || ModelState["Threshold"].Errors.Count != 0
                || ModelState["LearningRate"].Errors.Count != 0 || ModelState["MaxIterationNumberInDeltaRule"].Errors.Count != 0)
            {
                perceptronVM.DisplayResult = false;
            }
            else
            {
                perceptronVM.DisplayResult = true;
            }


            if (ModelState.IsValid)
            {
                PerceptronBusinessLayer perceptronBL = new PerceptronBusinessLayer();
                switch (btnSubmit)
                {
                    case "Oblicz wagi":
                        TrainingSet ts = new TrainingSet();

                        ts = perceptronBL.GetTrainingSet(10, perceptronVM.NumberOfExamplesInTrainingSet, -100, 101);
                        DeltaRule dr = perceptronBL.DeltaRule(ts, perceptronVM.LearningRate, perceptronVM.Threshold, perceptronVM.MaxIterationNumberInDeltaRule);
                        perceptronVM.IterationNumberInDeltaRule = dr.IterationNumber;
                        perceptronVM.WeightVector = perceptronBL.ConvertDoubleArrayToNullableDoubleArray(dr.WeightVector);
                        for (int i = 0; i < perceptronVM.WeightVector.Length; i++)
                        {
                            perceptronVM.WeightVector[i] = Math.Round((double)perceptronVM.WeightVector[i], 3);
                        }
                        perceptronVM.NewThreshold = Math.Round(dr.Threshold, 3);
                        break;

                    case "OK":
                        if ((perceptronVM.XCoordinate != null) == false)
                        {
                            ModelState.AddModelError("XCoordinate", "Pole Współrzędna X jest wymagane");
                        }
                        if ((perceptronVM.YCoordinate != null) == false)
                        {
                            ModelState.AddModelError("YCoordinate", "Pole Współrzędna Y jest wymagane");
                        }
                        if (perceptronVM.XCoordinate != null && perceptronVM.YCoordinate != null)
                        {
                            double[] coordinates = new double[2];
                            coordinates[0] = (double)perceptronVM.XCoordinate;
                            coordinates[1] = (double)perceptronVM.YCoordinate;

                            if (perceptronBL.IsActivation(coordinates, perceptronBL.ConvertNullableDoubleArrayToDoubleArray(perceptronVM.WeightVector), (double)perceptronVM.NewThreshold) == 0)
                            {
                                perceptronVM.Output = "Brak aktywacji";
                            }
                            else
                            {
                                perceptronVM.Output = "Nastąpiła aktywacja";
                            }
                        }
                        break;
                }
            }

            return View(perceptronVM);
        }


        #region k-means
        public ActionResult KMeans()
        {
            KMeansViewModel kMeansVM = new KMeansViewModel();

            InitPointListForKMeansViewModel(kMeansVM);
            InitCentroidListForKMeansViewModel(kMeansVM);

            string errorMessage = (string)TempData["enterDataError"];
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("enterDataError", errorMessage);
            }

            return View(kMeansVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KMeans(KMeansViewModel kMeansVM)
        {
            KMeansBusinessLayer kMeansBL = new KMeansBusinessLayer();

            //check validation for input data
            if (kMeansVM.SelectedMethod == "random")
            {
                string errorMessage = kMeansBL.InputDataAreValid(kMeansVM.PointsNumber, kMeansVM.CentroidsNumber, kMeansVM.Dimnesion);
                if (string.IsNullOrEmpty(errorMessage) == false)
                {
                    ModelState.AddModelError("randomMethod", errorMessage);
                }
                else
                {
                    PointsAndCentroids pac = new PointsAndCentroids();
                    pac = kMeansBL.RandomInputData(kMeansVM.PointsNumber, kMeansVM.CentroidsNumber, kMeansVM.Dimnesion);

                    kMeansVM.PointList = pac.PointList;
                    kMeansVM.CentroidList = pac.CentroidList;
                }
            }
            else if (kMeansVM.SelectedMethod == "manually")
            {
                string errorMessage1 = 
                    kMeansBL.InputDataAreValid(kMeansVM.PointList.Count, kMeansVM.CentroidList.Count, kMeansVM.PointList[0].Coordinate.Length);
                string errorMessage2 = kMeansBL.InputDataAreValid(kMeansVM.PointList, kMeansVM.CentroidList);
                if (string.IsNullOrEmpty(errorMessage1) == false)
                {
                    ModelState.AddModelError("manuallyMethod1", errorMessage1);
                }
                if (string.IsNullOrEmpty(errorMessage2) == false)
                {
                    ModelState.AddModelError("manuallyMethod2", errorMessage1);
                }

                //if no error, set groups for centroids
                if (ModelState.IsValid)
                {
                    for(int i = 0; i < kMeansVM.CentroidList.Count; i++)
                    {
                        kMeansVM.CentroidList[i].GroupNumber = (i + 1);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                TempData["pointList"] = kMeansVM.PointList;
                TempData["centroidList"] = kMeansVM.CentroidList;

                return RedirectToAction("KMeansResult");
            }
            else
            {
                //set selected method
                kMeansVM.DataEntryMethods = kMeansBL.SetValuesToFalseInDict(kMeansVM.DataEntryMethods);
                kMeansVM.DataEntryMethods[kMeansVM.SelectedMethod] = true;

                //initialize object if object is null
                bool pointListIsNotNull = kMeansVM.PointList != null;
                bool centroidListIsNotNull = kMeansVM.CentroidList != null;
                if (pointListIsNotNull == false)
                {
                    InitPointListForKMeansViewModel(kMeansVM);
                }
                if (centroidListIsNotNull == false)
                {
                    InitCentroidListForKMeansViewModel(kMeansVM);
                }
            }

            return View(kMeansVM);
        }

        public ActionResult KMeansResult()
        {
            KMeansBusinessLayer kMeansBL = new KMeansBusinessLayer();
            KMeansResultViewModel kMeansResultVM = new KMeansResultViewModel();
            kMeansResultVM.inputPointList = new List<Point>();
            kMeansResultVM.inputCentroidList = new List<Centroid>();

            kMeansResultVM.inputPointList = (List<Point>)TempData["pointList"];
            kMeansResultVM.inputCentroidList = (List<Centroid>)TempData["centroidList"];

            if ((kMeansResultVM.inputPointList != null && kMeansResultVM.inputCentroidList != null) == false)
            {
                TempData["enterDataError"] = "Podaj dane";
                return RedirectToAction("Kmeans");
            }

            kMeansResultVM = kMeansBL.KMeansAlgorithm(kMeansResultVM.inputPointList, kMeansResultVM.inputCentroidList);

            return View(kMeansResultVM);
        }

        [NonAction]
        public void InitPointListForKMeansViewModel(KMeansViewModel kMeansVM)
        {
            kMeansVM.PointList = new List<Point>();

            //add 3 2D points
            kMeansVM.PointList.Add(new Point(-1, new double[2]));
            kMeansVM.PointList.Add(new Point(-1, new double[2]));
            kMeansVM.PointList.Add(new Point(-1, new double[2]));
        }

        [NonAction]
        public void InitCentroidListForKMeansViewModel(KMeansViewModel kMeansVM)
        {
            kMeansVM.CentroidList = new List<Centroid>();

            //add 2 2D centroids
            kMeansVM.CentroidList.Add(new Centroid(1, new double[2]));
            kMeansVM.CentroidList.Add(new Centroid(2, new double[2]));
        }
        #endregion
    }
}