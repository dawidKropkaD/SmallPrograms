using SmallPrograms.Areas.DataMining.Models;
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

                        ts = perceptronBL.GetTrainingSet(perceptronVM.Threshold, perceptronVM.NumberOfExamplesInTrainingSet, -100, 101);
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
                        if (perceptronVM.XCoordinate == null)
                        {
                            ModelState.AddModelError("XCoordinate", "Pole Współrzędna X jest wymagane");
                        }
                        if (perceptronVM.YCoordinate == null)
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
            return View(new KMeansViewModel(3, 2, 2));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KMeansResult([Bind(Include = "PointList,CentroidList,PointsNumber,CentroidsNumber,Dimnesion,SelectedMethod,DataEntryMethods")] KMeansViewModel kMeansVM)
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
                    for (int i = 0; i < kMeansVM.CentroidList.Count; i++)
                    {
                        kMeansVM.CentroidList[i].GroupNumber = (i + 1);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                KMeansResultViewModel kMeansResultVM = new KMeansResultViewModel();

                kMeansResultVM = kMeansBL.KMeansAlgorithm(kMeansVM.PointList, kMeansVM.CentroidList);

                return View(kMeansResultVM);
            }
            else
            {
                kMeansBL.SetValuesToFalseInDict(kMeansVM.DataEntryMethods);
                kMeansVM.DataEntryMethods[kMeansVM.SelectedMethod] = true;
                var tmp = kMeansVM.DataEntryMethods;

                if (kMeansVM.PointList==null || kMeansVM.CentroidList== null)
                {
                    kMeansVM = new KMeansViewModel(3, 2, 2); 
                }
                kMeansVM.DataEntryMethods = tmp;
                                
                return View("KMeans", kMeansVM);
            }
        }
        #endregion
    }
}