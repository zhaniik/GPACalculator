namespace GPACalculator.Models
{
    public class AddCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Grade { get; set; }
        public AddCourse(int id, string name, int credits, string grade)
        {
            Id = id;
            Name = name;
            Credits = credits;
            Grade = grade;
        }   
    }
}
