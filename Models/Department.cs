using System.Text.Json.Serialization;

namespace prac.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Building { get; set; } = string.Empty;

    // Navigation Properties
    [JsonIgnore]
    public ICollection<Course> Courses { get; set; } = new List<Course>();

    [JsonIgnore]
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
