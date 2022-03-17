using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDATAManagerCore.Model
{
    public class UserExerciescs
    {
        public string AuthUserId { get; set; }
        public int workoutId { get; set; }
        public string Name { get; set; }
        public bool MetricType { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public string CreateTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Score { get; set; }
        public bool follow { get; set; }

    }
}
