using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prac.Data;
using prac.Models;

namespace prac.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students
            .Include(s => s.Department)
            .Include(s => s.Course)
            .ToListAsync();
    }

    // GET: api/students/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students
            .Include(s => s.Department)
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return NotFound();

        return student;
    }

    // POST: api/students (FluentValidation automatically validates CreateStudentDto)
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent([FromBody] CreateStudentDto dto)
    {
        var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId);
        if (!departmentExists) return BadRequest($"Department with Id {dto.DepartmentId} does not exist.");

        if (dto.CourseId.HasValue)
        {
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == dto.CourseId.Value);
            if (!courseExists) return BadRequest($"Course with Id {dto.CourseId.Value} does not exist.");
        }

        var student = new Student
        {
            RollNumber = dto.RollNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Age = dto.Age,
            Gpa = dto.Gpa,
            DepartmentId = dto.DepartmentId,
            CourseId = dto.CourseId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    // PUT: api/students/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] CreateStudentDto dto)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        student.RollNumber = dto.RollNumber;
        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.Email = dto.Email;
        student.Age = dto.Age;
        student.Gpa = dto.Gpa;
        student.DepartmentId = dto.DepartmentId;
        student.CourseId = dto.CourseId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
