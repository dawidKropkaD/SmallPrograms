using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class PerceptronBusinessLayer
    {
        /// <summary>
        /// Get training set
        /// </summary>
        /// <param name="t">threshold (próg)</param>
        /// <param name="numberOfExamples">number of examples in training set</param>
        /// <param name="minValue">minimum value of vector and weight</param>
        /// <param name="maxValue">maximum value of vector and weight</param>
        /// <returns>Training set</returns>
        public TrainingSet GetTrainingSet(double t, int numberOfExamples, int minValue, int maxValue)
        {
            Random r = new Random();
            TrainingSet ts = new TrainingSet();
            ts.ExampleList = new List<TrainingSet.SingleExample>();

            for (int i = 0; i < numberOfExamples; i++)
            {
                TrainingSet.SingleExample example = new TrainingSet.SingleExample();
                example.InputVector = new double[2];

                example.InputVector[0] = r.Next(minValue, maxValue);
                example.InputVector[1] = r.Next(minValue, maxValue);

                if (example.InputVector[0] + example.InputVector[1] > t)
                {
                    example.Output = 1;
                }
                else
                {
                    example.Output = 0;
                }
                ts.ExampleList.Add(example);
            }

            return ts;
        }

        /// <summary>
        /// Calculates delta rule
        /// </summary>
        /// <param name="ts">Training set</param>
        /// <param name="learningRate">Learning rate</param>
        /// <param name="threshold">Threshold</param>
        /// <param name="maxIterationNumber">Maximum number of iteration while calculating weight vector
        /// (Maksymalna liczba iteracji podczas obliczania wektora wag)</param>
        /// <returns>Delta rule</returns>
        public DeltaRule DeltaRule(TrainingSet ts, double learningRate, double threshold, int maxIterationNumber)
        {
            double errorRate = 0.02;    //współczynnik błędu
            int changeNumber = 0;
            int iterationNumber = 0;
            double[] weightVector = new double[2];
            //set random weight
            Random r = new Random();
            weightVector[0] = -10;
            weightVector[1] = 20;

            do
            {
                iterationNumber++;
                changeNumber = 0;
                for (int i = 1; i < ts.ExampleList.Count(); i++)
                {
                    int y = IsActivation(ts.ExampleList[i].InputVector, weightVector, threshold);
                    for (int j = 0; j < weightVector.Length; j++)
                    {
                        weightVector[j] = weightVector[j] + learningRate * (ts.ExampleList[i].Output - y) * ts.ExampleList[i].InputVector[j];
                        threshold = threshold - learningRate * (ts.ExampleList[i].Output - y);
                    }
                    if (ts.ExampleList[i].Output - y != 0)
                    {
                        changeNumber++;
                    }
                }
            }
            while ((double)changeNumber / ts.ExampleList.Count > errorRate && iterationNumber < maxIterationNumber);

            DeltaRule dr = new DeltaRule();
            dr.WeightVector = weightVector;
            dr.Threshold = threshold;
            dr.IterationNumber = iterationNumber;

            return dr;
        }

        /// <summary>
        /// Check if perceptron is active or not
        /// </summary>
        /// <param name="inputVector">Input vector</param>
        /// <param name="weightVector">Weight vector</param>
        /// <param name="threshold">Threshold</param>
        /// <returns>1 if perceptron is active, otherwise 0</returns>
        public int IsActivation(double[] inputVector, double[] weightVector, double threshold)
        {
            double net = 0;

            for (int i = 0; i < inputVector.Length; i++)
            {
                net += inputVector[i] * weightVector[i];
            }
            if (net > threshold)
            {
                return 1;
            }
            return 0;
        }

        public double?[] ConvertDoubleArrayToNullableDoubleArray(double[] array)
        {
            double?[] nullableArray = new double?[array.Length];
            for(int i = 0; i < array.Length; i++)
            {
                nullableArray[i] = array[i];
            }

            return nullableArray;
        }

        public double[] ConvertNullableDoubleArrayToDoubleArray(double?[] nullableArray)
        {
            double[] array = new double[nullableArray.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (double)nullableArray[i];
            }

            return array;
        }
    }
}