using SchoolManagement.DAL.Entities;
using SchoolManagement.DAL.Repositories;
namespace SchoolManagement.BAL.Services
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            return await _studentRepository.GetStudentAsync(id);
        }
    }
}
