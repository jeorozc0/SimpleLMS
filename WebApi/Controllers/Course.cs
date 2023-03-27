using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly List<Course> _courses = new List<Course>()
        {
            new Course() { ID = 1, Name = "Course 1" },
            new Course() { ID = 2, Name = "Course 2" },
            new Course() { ID = 3, Name = "Course 3" },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetAllCourses()
        {
            return Ok(_courses);
        }

        [HttpGet("{courseId}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            var course = _courses.Find(c => c.ID == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public ActionResult<Course> CreateCourse(Course course)
        {
            _courses.Add(course);

            return CreatedAtAction(nameof(GetCourseById), new { id = course.ID }, course);
        }

        [HttpPut("{courseId}")]
        public ActionResult UpdateCourse(int id, Course course)
        {
            var existingCourse = _courses.Find(c => c.ID == id);

            if (existingCourse == null)
            {
                return NotFound();
            }

            existingCourse.Name = course.Name;

            return NoContent();
        }

        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourse(int id)
        {
            var courseToRemove = _courses.Find(c => c.ID == id);

            if (courseToRemove == null)
            {
                return NotFound();
            }

            _courses.Remove(courseToRemove);

            return NoContent();
        }
    }

    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
