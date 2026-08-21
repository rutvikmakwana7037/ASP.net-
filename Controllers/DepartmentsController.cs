using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prac.Data;
using prac.Models;

namespace prac.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await _context.Departments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await _context.Departments
            .Include(d => d.Courses)
            .Include(d => d.Students)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department == null) return NotFound();

        return department;
    }

    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment([FromBody] CreateDepartmentDto dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            Code = dto.Code,
            Building = dto.Building
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
    }
}
