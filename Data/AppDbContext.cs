using Microsoft.EntityFrameworkCore;
using prac.Models;

namespace prac.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Department Entity
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Code).IsRequired().HasMaxLength(10);
            entity.Property(d => d.Building).HasMaxLength(100);
            entity.HasIndex(d => d.Code).IsUnique();
        });

        // Configure Course Entity
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.CourseCode).IsRequired().HasMaxLength(20);
            entity.Property(c => c.Title).IsRequired().HasMaxLength(150);
            entity.HasIndex(c => c.CourseCode).IsUnique();

            entity.HasOne(c => c.Department)
                  .WithMany(d => d.Courses)
                  .HasForeignKey(c => c.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Student Entity
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.RollNumber).IsRequired().HasMaxLength(20);
            entity.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(s => s.LastName).IsRequired().HasMaxLength(50);
            entity.Property(s => s.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(s => s.RollNumber).IsUnique();
            entity.HasIndex(s => s.Email).IsUnique();

            entity.HasOne(s => s.Department)
                  .WithMany(d => d.Students)
                  .HasForeignKey(s => s.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Course)
                  .WithMany(c => c.Students)
                  .HasForeignKey(s => s.CourseId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Seed initial data
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Computer Science", Code = "CS", Building = "Tech Hall 1" },
            new Department { Id = 2, Name = "Information Technology", Code = "IT", Building = "Tech Hall 2" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, CourseCode = "CS101", Title = "Introduction to C# & .NET", Credits = 4, DepartmentId = 1 },
            new Course { Id = 2, CourseCode = "IT202", Title = "Database Management Systems", Credits = 3, DepartmentId = 2 }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                RollNumber = "STU-001",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@university.edu",
                Age = 21,
                Gpa = 3.8,
                DepartmentId = 1,
                CourseId = 1,
                CreatedAt = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
