using GPACalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPACalculator.Services
{
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
            if (course.Key=="")
            {
                throw new Exception($"Course with name {name} not found.");
            }

            var result= StaticData.nameofCourses.Remove(course.Key);
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