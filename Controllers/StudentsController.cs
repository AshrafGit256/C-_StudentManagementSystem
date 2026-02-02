using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;

namespace StudentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    // Temporary in-memory list (we'll replace this with a database later)
    private static List<Student> students = new()
    {
        new Student
        {
            Id = 1,
            Name = "John Doe",
            Email = "john@example.com",
            Age = 20,
            Course = "Computer Science",
            EnrollmentDate = new DateTime(2024, 9, 1)
        },
        new Student
        {
            Id = 2,
            Name = "Jane Smith",
            Email = "jane@example.com",
            Age = 22,
            Course = "Engineering",
            EnrollmentDate = new DateTime(2023, 9, 1)
        }
    };

    // GET: api/students
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        return Ok(students);
    }

    // GET: api/students/1
    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound($"Student with ID {id} not found");

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        // Generate new ID
        student.Id = students.Any() ? students.Max(s => s.Id) + 1 : 1;

        students.Add(student);

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    // PUT: api/students/1
    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
    {
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound($"Student with ID {id} not found");

        student.Name = updatedStudent.Name;
        student.Email = updatedStudent.Email;
        student.Age = updatedStudent.Age;
        student.Course = updatedStudent.Course;
        student.EnrollmentDate = updatedStudent.EnrollmentDate;

        return Ok(student);
    }

    // DELETE: api/students/1
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound($"Student with ID {id} not found");

        students.Remove(student);

        return Ok(new { message = $"Student {student.Name} deleted successfully" });
    }

}