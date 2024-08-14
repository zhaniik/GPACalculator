namespace GPACalculator.Models
{
    public class Course
    {
        public string Name { get; set; }
        public string Grade { get; set; }
        public Course(string name, string grade)
        {
            Name = name;
            Grade = grade;
        }
    }

    public class StaticData
    {
        public static readonly Dictionary<string, double> gradePoints = new Dictionary<string, double>
        {
            {"A+", 4.3}, {"A", 4.0}, {"A-", 3.7},
            {"B+", 3.3}, {"B", 3.0}, {"B-", 2.7},
            {"C+", 2.3}, {"C", 2.0}, {"C-", 1.7},
            {"D+", 1.3}, {"D", 1.0}, {"D-", 0.7},
            {"F" , 0.0}
        };

        public static readonly Dictionary<string, int> nameofCourses = new Dictionary<string, int>
        { 
            {"Algebra",  4},
            {"Physics",  4},
            {"PE",       2},
            {"History",  3},
            {"English",  3},
            {"Geometry", 4}
        };

        public static List<Course> courses = new List<Course>();
    }
}

