using Microsoft.EntityFrameworkCore;
using SchoolManagement.DAL.DataContext;
using SchoolManagement.DAL.Entities;

namespace SchoolManagement.DAL.Repositories
{
    public class StudentRepository
    {
        private readonly SchoolManagementDbContext _context;

        public StudentRepository(SchoolManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

    }
}
