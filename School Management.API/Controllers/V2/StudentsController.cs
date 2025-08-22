using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BAL.Services;
using SchoolManagement.BAL.Utilities;

namespace School_Management.API.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("getstudent")]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            try
            {
                return Ok(await _studentService.GetStudentAsync(studentId));
            }
            catch (UserNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
