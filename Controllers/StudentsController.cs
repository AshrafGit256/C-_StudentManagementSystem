using Microsoft.AspNetCore.Mvc;

namespace StudentAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    // GET: api/students
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = new[]
        {
            new { Id = 1, Name = "John Doe", Email = "john@example.com", Age = 20 },
            new { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Age = 22 }
        };
        
        return Ok(students);
    }
}
