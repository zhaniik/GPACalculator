using GPACalculator.Models;
using GPACalculator.Services;
using Microsoft.AspNetCore.Mvc;

namespace GPACalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPACalculatorController : ControllerBase
    {
        private readonly GPACalculatorService _gpaCalculatorService;
        private readonly DbManager _dbManager;
        private readonly ILogger<GPACalculatorController> _logger;

        public GPACalculatorController(ILogger<GPACalculatorController> logger)
        {
            _gpaCalculatorService = new GPACalculatorService();
            _dbManager= new DbManager();
            _logger = logger;
        }

        [HttpPost]
        [Route("AddGrade")]
        public IActionResult AddGrade([FromBody] List<Course> courses)
        {
            if (courses == null || courses.Count == 0)
            {
                return BadRequest("Course list is null or empty.");
            }
            try
            {
                double gpa = _gpaCalculatorService.CalculateGPA(courses);
                return Ok(new { GPA = gpa });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid data in AddGrade");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCourses")]
        public IActionResult GetCourses()
        {
            var courses = _gpaCalculatorService.GetCourses();
            return Ok(courses);
        }


        [HttpGet]
        [Route("GetName")]
        public IActionResult GetName()
        {
            var courses = _dbManager.GetNameOfCoursesData();
            return Ok(courses);
        }



        [HttpGet]
        [Route("GetAvailableCourses")]
        public IActionResult GetAvailableCourses()
        {
            var availableCourses = _gpaCalculatorService.GetAvailableCourses();
            return Ok(availableCourses);
        }

        [HttpDelete]
        [Route("DeleteCourse/{name}")]
        public IActionResult DeleteCourse(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Course name is null or empty.");
            }
            try
            {
                _gpaCalculatorService.DeleteCourse(name);
                return Ok(new { Message = "Course deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid course name in DeleteCourse");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Course is null.");
            }
            try
            {
                _gpaCalculatorService.UpdateCourse(course);
                return Ok(new { Message = "Course updated" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Invalid data in UpdateCourse");
                return BadRequest(ex.Message);
            }
        }
    }
}