using JSON_DAL;

namespace JsonDemo.Models
{
public class DB
{
    #region singleton setup
    private static readonly DB instance = new DB();
    public static DB Instance { get { return instance; } }
    #endregion
    #region Repositories

    static public StudentsRepository Students { get; set; } 
        = new StudentsRepository();

    static public CoursesRepository Courses { get; set; } 
        = new CoursesRepository();

    static public Repository<Registration> Registrations { get; set; } 
        = new Repository<Registration>();

    #endregion
}
}