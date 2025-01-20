using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Controllers;

public class StudentController : ControllerBase
{
    public List<Student> Students = new List<Student>()
    {
        new Student()
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            Address = "London",
            PhoneNumber = "08888888888"
        },
        new Student()
        {
            Name = "John",
            Email = "john.doe@gmail.com",
            Address = "London",
            PhoneNumber = "08888888888"
        }
    };





    [HttpPost("filter/students")]
    public IActionResult RegisterForm([FromQuery] string name)
    {
        var student = Students.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();     
        return Ok(student);
    }
}

public class Student 
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

}