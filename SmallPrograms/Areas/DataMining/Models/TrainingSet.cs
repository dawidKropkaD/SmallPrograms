using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class TrainingSet
    {
        public List<SingleExample> ExampleList { get; set; }

        public class SingleExample
        {
            public double[] InputVector { get; set; }
            public int Output { get; set; }
        }
    }
}