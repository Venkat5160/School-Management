using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.DAL.DataContext;
using SchoolManagement.DAL.Entities;

namespace School_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolManagementDbContext _context;

        public StudentsController(SchoolManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // api/Students/11?iid=11&name=22'
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id,[FromQuery] string name, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Parameter: Student student (model binding from body, but no explicit [FromBody] attribute)
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            var isLinq = false;
            if (isLinq)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Optional: LINQ-style check to avoid duplicate entry
                var existing = _context.Students
                    .FirstOrDefault(s => s.Name == student.Name && s.DateOfBirth == student.DateOfBirth && s.Address == student.Address);
                if (existing != null)
                {
                    return Conflict("Student already exists.");
                }
                // Add student
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //	Parameter: [FromBody] Student student (explicitly binds from request body)
        //Adds a new Student(POST api/student).
        [HttpPost("create")]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchStudent([FromQuery] string? name, [FromQuery] string? address, [FromQuery] string? phoneNumber)
        {
            var filteredStudents = _context.Students.AsQueryable();
            if (!string.IsNullOrEmpty(name))
                filteredStudents = filteredStudents.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(address))
                filteredStudents = filteredStudents.Where(e => e.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(phoneNumber))
                filteredStudents = filteredStudents.Where(e => e.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            var result = await filteredStudents.ToListAsync();
            if (!result.Any())
                return NotFound("No student match the provided search criteria.");
            return Ok(result);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
