using Microsoft.AspNetCore.Mvc;

namespace dotent.Controllers;

public class TaskManagementController : ControllerBase
{
    public static List<Member> Members = new List<Member>();

    [HttpPost("/api/member")]
    public IActionResult Create([FromBody] MemberDto memberDto)
    {
        var member = new Member
        {
            Id = Members.Count + 1,
            FirstName = memberDto.FirstName,
            Email = memberDto.Email,
            PhoneNumber = memberDto.PhoneNumber,
            Address = memberDto.Address
        };

        Members.Add(member);
        return Ok("Member created successfully");
    }


    [HttpGet("/api/member")]
    public IActionResult GetAll([FromQuery] MemberFilterDto filter)
    {
        var members = Members.Where(x =>
            (string.IsNullOrEmpty(filter.FirstName) || x.FirstName.Contains(filter.FirstName))
            && (string.IsNullOrEmpty(filter.Email) || x.Address.Contains(filter.Email))
        ).ToList();
        return Ok(members);
    }

    [HttpGet("/api/member/{id}")]
    public IActionResult GetById(int id)
    {
        var member = Members.FirstOrDefault(x => x.Id == id);
        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPut("/member/{id}")]

    public IActionResult Update(int Id, [FromBody] MemberDto memberDto)
    {
        var member = Members.FirstOrDefault(x => x.Id == Id);
        if (member == null)
        {
            return NotFound();
        }
        member.FirstName = memberDto.FirstName;
        member.Email = memberDto.Email;
        member.PhoneNumber = memberDto.PhoneNumber;
        member.Address = memberDto.Address; 
        
        return Ok("Member updated successfully");
    }

    [HttpDelete("/api/member/{id}")]
    public IActionResult Delete(int id)
    {
        var member = Members.FirstOrDefault(x => x.Id == id);
        if (member == null)
        {
            return NotFound();
        }
        Members.Remove(member);
        return Ok("Member deleted successfully");
        
    }
}

public class MemberFilterDto
{
    public string FirstName { get; set; }
    
    public string Email { get; set; }
}
public class MemberDto
{
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}

public class Member
{
    public string FirstName { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string phone { get; set; }
}