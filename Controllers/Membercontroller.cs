
using Dapper;
using dotent.Repositories;
using dotent.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace dotent.Controllers;

public class MemberController : ControllerBase
{
    private readonly MemberService _memberService;
    private readonly MemberRepository _memberRepository;

    public MemberController(MemberService memberService, MemberRepository memberRepository)
    {
        _memberService = memberService;
        _memberRepository = memberRepository;
    }

    [HttpPost("/api/member")]
    public async Task<IActionResult> Create([FromBody] MemberDto memberDto)
    {
        try
        {
            await _memberService.CreateAsync(memberDto);
            return Ok("Member created successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/api/member/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var member = await _memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                return NotFound("Member not found");
            }

            return Ok(member);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("/api/member")]
    public async Task<IActionResult> GetAll([FromQuery] MemberFilterDto filter)
    {
        try
        {
            var members = await _memberRepository.GetAllAsync(filter.FirstName);
            return Ok(members);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPut("/api/member/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MemberDto memberDto)
    {
        //var exisingMember = Members.FirstOrDefault(x => x.Id == id);
        var existingMember = await _memberRepository.GetByIdAsync(id);

        if (existingMember == null)
        {
            return NotFound();
        }

        existingMember.FirstName = memberDto.FirstName;
        existingMember.Email = memberDto.Email;
        existingMember.Phonenumber  = memberDto.PhoneNumber;
        existingMember.Address = memberDto.Address;

        return Ok("Member updated successfully");
    }

    [HttpDelete("/api/member/{id}")]
    public  async Task<IActionResult> Delete(int id)
    {
        try
        {
            var  member = await _memberRepository.GetByIdAsync(id);;
            if (member == null)
            {
                return NotFound();
            }
            await _memberRepository.DeleteAsync(id);
            return Ok("Member deleted successfully");

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}


public class MemberFilterDto
{
    public string? FirstName { get; set; }
    public string? Address { get; set; }
}

public class MemberDto // Data Transfer Object
{
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}

public class Member
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Phonenumber  { get; set; }
    public string Address { get; set; }
}

public class BankAccount
{
    private readonly Member _member;

    public BankAccount(Member member)
    {
        _member = member;
    }

    public void Deposit(decimal amount)
    {
        // Deposit amount to the bank account
    }
}

public class ShareAccount
{
    private readonly BankAccount _account;

    public ShareAccount(BankAccount account)
    {
        _account = account;
    }

    public void Deposit(decimal amount)
    {
        // Deposit amount to the share account
    }
}
