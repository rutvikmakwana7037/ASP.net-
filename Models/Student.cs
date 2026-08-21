namespace prac.Models;

public class Student
{
    public int Id { get; set; }
    public string RollNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public double Gpa { get; set; }

    // Foreign Keys & Navigation Properties
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public int? CourseId { get; set; }
    public Course? Course { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
