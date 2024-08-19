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
}

