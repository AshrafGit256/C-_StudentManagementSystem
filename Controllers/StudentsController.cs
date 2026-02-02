using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;
using StudentAPI.Data;

namespace StudentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    // Constructor - injects the database context
    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/students
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    // GET: api/students/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return NotFound($"Student with ID {id} not found");

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    // PUT: api/students/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return NotFound($"Student with ID {id} not found");

        student.Name = updatedStudent.Name;
        student.Email = updatedStudent.Email;
        student.Age = updatedStudent.Age;
        student.Course = updatedStudent.Course;
        student.EnrollmentDate = updatedStudent.EnrollmentDate;

        await _context.SaveChangesAsync();

        return Ok(student);
    }

    // DELETE: api/students/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
            return NotFound($"Student with ID {id} not found");

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Student {student.Name} deleted successfully" });
    }
}