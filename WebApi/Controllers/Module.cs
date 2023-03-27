using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/course/{courseId}/modules")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly List<Module> _modules = new List<Module>()
        {
            new Module() { ID = 1, Name = "Module 1", CourseID = 1 },
            new Module() { ID = 2, Name = "Module 2", CourseID = 1 },
            new Module() { ID = 3, Name = "Module 3", CourseID = 2 },
            new Module() { ID = 4, Name = "Module 4", CourseID = 3 },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Module>> GetModulesByCourseId(int courseId)
        {
            var modules = _modules.FindAll(m => m.CourseID == courseId);

            if (modules == null)
            {
                return NotFound();
            }

            return Ok(modules);
        }

        [HttpGet("{moduleId}")]
        public ActionResult<Module> GetModuleById(int courseId, int id)
        {
            var module = _modules.Find(m => m.ID == id && m.CourseID == courseId);

            if (module == null)
            {
                return NotFound();
            }

            return Ok(module);
        }

        [HttpPost]
        public ActionResult<Module> CreateModule(int courseId, Module module)
        {
            module.ID = _modules.Count + 1;
            module.CourseID = courseId;
            _modules.Add(module);

            return CreatedAtAction(nameof(GetModuleById), new { courseId = module.CourseID, id = module.ID }, module);
        }

        [HttpPut("{moduleId}")]
        public ActionResult UpdateModule(int courseId, int id, Module module)
        {
            var existingModule = _modules.Find(m => m.ID == id && m.CourseID == courseId);

            if (existingModule == null)
            {
                return NotFound();
            }

            existingModule.Name = module.Name;

            return NoContent();
        }

        [HttpDelete("{moduleId}")]
        public ActionResult DeleteModule(int courseId, int id)
        {
            var module = _modules.Find(m => m.ID == id && m.CourseID == courseId);

            if (module == null)
            {
                return NotFound();
            }

            _modules.Remove(module);

            return NoContent();
        }
    }

    public class Module
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CourseID { get; set; }
    }
}