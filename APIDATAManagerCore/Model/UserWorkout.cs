using System;

namespace APIDataManager.Library.Models
{
    public class UserWorkout
    {
        public int Id { get; set; }
        public string AuthId { get; set; }
        public string Title { get; set; }
        public bool Public { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
