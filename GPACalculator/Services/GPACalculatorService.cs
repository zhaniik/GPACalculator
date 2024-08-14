using GPACalculator.Models;
using System.Data;
using System.Data.SqlClient;
using GPACalculator.Data;
namespace GPACalculator.Services
{
    public class DbManager
    {
        SqlConnection connection;
        SqlCommand command;
        public DbManager()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=ZHANIIK\SQLSERVER;
                                            Initial Catalog=GPACalculatorBase;Integrated Security=True;";
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
        }
        public string GetNameOfCoursesData()
        {
            var result = new List<string>();
            try
            {
                connection.Open();
                command.CommandText = "SELECT name_of_courses FROM Courses1"; 
                //command.CommandText = "SELECT name_of_courses FROM Courses1 WHERE name_of_courses = @name_of_courses AND credits = @credits";
                //command.Parameters.AddWithValue("@name_of_courses", name_of_courses.Text);
                //command.Parameters.AddWithValue("@credits", credits.Text);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(reader["name_of_courses"].ToString()!);
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                connection.Close();
            }
            return string.Join(", ", result); 
        }


    }
    public class GPACalculatorService 
    {    
        public double CalculateGPA(List<Course> pcourses)
        {
            StaticData.courses = pcourses;
            double totalPoints = 0;
            int totalCredits = 0;

            foreach (var course in StaticData.courses)
            {
                if (StaticData.gradePoints.ContainsKey(course.Grade) && StaticData.nameofCourses.ContainsKey(course.Name))
                {
                    totalPoints += StaticData.gradePoints[course.Grade] * StaticData.nameofCourses[course.Name];
                    totalCredits += StaticData.nameofCourses[course.Name];
                }
                else
                {
                    throw new ArgumentException($"Invalid grade or course name: {course.Grade} or {course.Name}");
                }
            }
            return totalCredits == 0 ? 0 : Math.Round(totalPoints / totalCredits, 3);
        }






        public List<Course> GetCourses()
        {
            return StaticData.courses;
        }

        public Dictionary<string, int> GetAvailableCourses()
        {
            return StaticData.nameofCourses;
        }

        public void DeleteCourse(string name)
        {
            var course = StaticData.nameofCourses.FirstOrDefault(c => c.Key.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (course.Key == "")
            {
                throw new Exception($"Course with name {name} not found.");
            }
            var result = StaticData.nameofCourses.Remove(course.Key);
        }
        public void UpdateCourse(Course course)
        {
            var existingCourse = StaticData.courses.FirstOrDefault(c => c.Name.Equals(course.Name, StringComparison.OrdinalIgnoreCase));
            if (existingCourse == null)
            {
                throw new ArgumentException($"Course with name {course.Name} not found.");
            }
            if (!StaticData.gradePoints.ContainsKey(course.Grade))
            {
                throw new ArgumentException($"Invalid grade: {course.Grade}");
            }
            existingCourse.Grade = course.Grade;
        }
    }
}