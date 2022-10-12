using CollegeSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Specialization> Specializations { get; set; }
    }
}
