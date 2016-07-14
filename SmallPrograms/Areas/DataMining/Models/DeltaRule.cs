using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.DataMining.Models
{
    public class DeltaRule
    {
        public double[] WeightVector { get; set; }
        public double Threshold { get; set; }
        public int IterationNumber { get; set; }
    }
}