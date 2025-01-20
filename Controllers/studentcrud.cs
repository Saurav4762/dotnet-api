using Dotnet.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

public class studentcrud : ControllerBase
{
    public static List<Student> Students = new List<Student>();

    [HttpPost("/api/student")]
    public IActionResult Create([FromBody] StudentDto studentDto)
    {
        var student = new Student
        {
            Id = Students.Count + 1,
            Name = studentDto.Name,
            Email = studentDto.Email,
            Phone = studentDto.Phone,
            Address = studentDto.Address,

        };
        
        Students.Add(student);
        return Ok("Students added successfully");
    }

    [HttpGet("/api/student")]

    public IActionResult GetAll([FromQuery] StudentFilterDto filter)
    {
        var students = Students.Where(x => 
            (string.IsNullOrEmpty(filter.Name) || x.Name.Contains (filter.Name))
            && (string.IsNullOrEmpty(filter.Email) || x.Email.Contains (filter.Email))
            ).ToList();
        return Ok(students);
    }

    [HttpGet("/api/student/{id}")]
    public IActionResult GetById(int id)
    {
        var student = Students.FirstOrDefault(x => x.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
        
    }

    [HttpPut("/api/student/{id}")]

    public IActionResult Update(int id, [FromBody] StudentDto studentDto)
    {
        var student = Students.FirstOrDefault(x => x.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        student.Name = studentDto.Name;
        student.Email = studentDto.Email;
        student.Phone = studentDto.Phone;
        student.Address = studentDto.Address;
        
        return Ok("Student updated successfully");
    }

    [HttpDelete("/api/student/{id}")]
    public IActionResult Delete(int id)
    {
        var student = Students.FirstOrDefault(x => x.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        Students.Remove(student);
        return Ok("Student deleted successfully");
    }
    
}

public class StudentFilterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
}
public class StudentDto //Data tramsfer object
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
}
public class Student //Model
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}