using Day1.Models;
using Microsoft.EntityFrameworkCore;
namespace Day1.Entity
{
    public class FacultyContext :DbContext
    {
        public FacultyContext() { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public FacultyContext(DbContextOptions option):base(option) 
        {
            
        }
    }
}
