namespace prac.Models;

public class CreateStudentDto
{
    public string RollNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public double Gpa { get; set; }
    public int DepartmentId { get; set; }
    public int? CourseId { get; set; }
}
