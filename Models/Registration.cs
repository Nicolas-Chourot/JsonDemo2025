using JSON_DAL;
using Newtonsoft.Json;

namespace JsonDemo.Models
{
    public class Registration : Record
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        [JsonIgnore] public Course Course { get { return DB.Courses.Get(CourseId); } }
        [JsonIgnore] public Student Student { get { return DB.Students.Get(StudentId); } }
    }
}