using System;
namespace APIDataManager.Library.Models
{
    public class UserExercies
    {
        public string AuthId { get; set; }
        public int workoutId { get; set; }
        public string ExerciseTypeId { get; set; }
        public bool MetricType { get; set; }
        public DateTime ExerciesCreateTime { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime SetUpdateTime { get; set; }
        public decimal Weight { get; set; }
    }
}
