using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prac.Data;
using prac.Models;

namespace prac.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses.Include(c => c.Department).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Department)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return NotFound();

        return course;
    }

    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse([FromBody] CreateCourseDto dto)
    {
        var department = await _context.Departments.FindAsync(dto.DepartmentId);
        if (department == null) return BadRequest($"Department with Id {dto.DepartmentId} does not exist.");

        var course = new Course
        {
            CourseCode = dto.CourseCode,
            Title = dto.Title,
            Credits = dto.Credits,
            DepartmentId = dto.DepartmentId
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }
}
