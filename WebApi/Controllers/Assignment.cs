using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/courses/{courseId}/modules/{moduleId}/assignment")]
public class AssignmentController : ControllerBase
{
    private static List<Assignment> _assignments = new List<Assignment>
    {
        new Assignment { ID = 1, Name = "Assignment 1", Grade = 100, DueDate = new DateTime(2020, 12, 31) },
        new Assignment { ID = 2, Name = "Assignment 2", Grade = 77, DueDate = new DateTime(2020, 12, 09) },
        new Assignment { ID = 3, Name = "Assignment 3", Grade = 88, DueDate = new DateTime(2020, 12, 22) },
    };

    [HttpGet]
    public ActionResult<Assignment> GetAssignmentsInModule(int courseId, int moduleId)
    {
        var assignment = _assignments.Find(a => a.ID == moduleId);

        if (assignment == null)
        {
            return NotFound();
        }

        return Ok(assignment);

    }

    [HttpGet("{assignmentId}")]
    public ActionResult<Assignment> GetAssignment(int courseId, int moduleId, int assignmentId)
    {
        var assignment = _assignments.Find(a => a.ID == assignmentId);

        if (assignment == null)
        {
            return NotFound();
        }

        return Ok(assignment);
    }

    [HttpPost]
    public ActionResult<Assignment> CreateAssignment(int courseId, int moduleId, Assignment assignment)
    {
        _assignments.Add(assignment);
        return CreatedAtAction(nameof(GetAssignment), new { courseId = courseId, moduleId = moduleId, assignmentId = assignment.ID }, assignment);
    }

    [HttpPut("{assignmentId}")]
    public ActionResult<Assignment> UpdateAssignment(int courseId, int moduleId, int assignmentId, Assignment assignment)
    {
        var existingAssignment = _assignments.Find(a => a.ID == assignmentId);

        if (existingAssignment == null)
        {
            return NotFound();
        }

        existingAssignment.Name = assignment.Name;
        existingAssignment.Grade = assignment.Grade;
        existingAssignment.DueDate = assignment.DueDate;

        return NoContent();
    }

    [HttpDelete("{assignmentId}")]
    public ActionResult<Assignment> DeleteAssignment(int courseId, int moduleId, int assignmentId)
    {
        var assignment = _assignments.Find(a => a.ID == assignmentId);

        if (assignment == null)
        {
            return NotFound();
        }

        _assignments.Remove(assignment);

        return NoContent();
    }



    public class Assignment
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Grade { get; set; }
    public DateTime DueDate { get; set; }

}
}
