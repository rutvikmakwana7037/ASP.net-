using System.Text.Json.Serialization;

namespace prac.Models;

public class Course
{
    public int Id { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Credits { get; set; }

    // Foreign Key & Navigation Property
    public int DepartmentId { get; set; }
    
    [JsonIgnore]
    public Department? Department { get; set; }

    [JsonIgnore]
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
