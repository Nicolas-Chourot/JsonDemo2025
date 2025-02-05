using JSON_DAL;
using System.Collections.Generic;

namespace JsonDemo.Models
{
    public class StudentsRepository : Repository<Student>
    {
        public bool Update(Student student, List<int> selectedCoursesId)
        {
            BeginTransaction();
            var result = base.Update(student);
            if (result) student.UpdateRegistrations(selectedCoursesId);
            EndTransaction();
            return result;
        }
        public override bool Delete(int Id)
        {
            Student student = DB.Students.Get(Id);
            if (student != null)
            {
                BeginTransaction();
                student.DeleteAllRegistrations();
                var result = base.Delete(Id);
                EndTransaction();
                return result;
            }
            return false;
        }
    }
}