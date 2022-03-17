using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDATAManagerCore.Model
{
    public class ExerciseSubmit
    {
        public string ExerciseType { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
        public bool MetricType { get; set; }
        public decimal Weight { get; set; }
    }
}
