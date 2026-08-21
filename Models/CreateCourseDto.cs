namespace prac.Models;

public class CreateCourseDto
{
    public string CourseCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int DepartmentId { get; set; }
}
