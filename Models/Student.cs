using JSON_DAL;
using MDB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JsonDemo.Models
{
    public class Student : Record
    {
        public Student()
        {
            Code = GenerateCode();
        }
        private static string GenerateCode()
        {
            string code = DateTime.Now.Year.ToString();
            Random rnd = new Random();
            for (int i = 0; i < 5; i++) code += rnd.Next(0, 9).ToString();
            return code;
        }
        public string Code { get; set; }
        [Display(Name = "Prénom"), Required(ErrorMessage = "Obligatoire")]
        public string FirstName { get; set; }
        [Display(Name = "Nom"), Required(ErrorMessage = "Obligatoire")]
        public string LastName { get; set; }
        [JsonIgnore]
        public string Caption
        {
            get { return Code + " " + LastName + " " + FirstName; }
        }
        [JsonIgnore]
        public int Year
        {
            get
            {
                return int.Parse(Code.Substring(0,4));
            }
        }
        [JsonIgnore]
        public List<Registration> Registrations
        {
            get
            {
                return DB.Registrations.ToList().Where(r => r.StudentId == Id).ToList();
            }
        }
        [JsonIgnore]
        public List<Course> Courses
        {
            get
            {
                var courses = new List<Course>();
                foreach (var registration in Registrations.OrderBy(r => r.Course.Code))
                {
                    courses.Add(registration.Course);
                }
                return courses;
            }
        }
        [JsonIgnore]
        public SelectList CoursesSelectList { get { return SelectListUtilities<Course>.Convert(Courses, "Caption"); } }

        public void DeleteAllRegistrations()
        {
            foreach (Registration registration in Registrations)
                DB.Registrations.Delete(registration.Id);
        }
        public void UpdateRegistrations(List<int> selectedCoursesId)
        {
            DeleteAllRegistrations();
            if (selectedCoursesId != null)
                foreach (int courseId in selectedCoursesId)
                    DB.Registrations.Add(new Registration { StudentId = Id, CourseId = courseId });
        }
    }
}