namespace SchoolManagement.DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        // Navigation properties
        public virtual ICollection<Course> Courses { get; set; }
        public Student()
        {
            Courses = new HashSet<Course>();
        }
    }
}
