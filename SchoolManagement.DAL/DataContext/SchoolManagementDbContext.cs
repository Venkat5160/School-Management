using Microsoft.EntityFrameworkCore;
using SchoolManagement.DAL.Entities;

namespace SchoolManagement.DAL.DataContext
{
    public class SchoolManagementDbContext : DbContext
    {
        //crate DbSet for each entity
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Venkanna", DateOfBirth = new DateTime(2025, 7, 26, 11, 0, 3, 1, DateTimeKind.Utc).AddTicks(9780), Address = "ABC", PhoneNumber = "8008850246" },
                new Student { Id = 2, Name = "Ravi", DateOfBirth = new DateTime(2025, 7, 26, 11, 0, 3, 1, DateTimeKind.Utc).AddTicks(9780), Address = "HDB", PhoneNumber = "89887677" }
            );
        }

    }
}
