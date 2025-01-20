using dotent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Controllers;

public class Bankcontroller: ControllerBase
{
    public static List<Account> Accounts = new List<Account>();

    [HttpPost("/api/account")]

    public IActionResult AddMember([FromForm] AccountDto memberDto)
    {
        var account = new Account
        {
            AccountId = Accounts.Count + 1,
            Email = memberDto.Email,
            AccountName = memberDto.AccountName,
            Address = memberDto.Address,
            phone = memberDto.phone,
        };
        
        Accounts.Add(account);
        return Ok("Account created sucessfully");
    }


    [HttpGet("/api/account")]

    public IActionResult GetAll([FromQuery] AccountFilterDto filter)
    {
        var account = Accounts.Where(x => 
            (string.IsNullOrEmpty(filter.Email ) || x.Email.Contains(filter.Email))
            &&(string.IsNullOrEmpty(filter.AccountName ) ||  x.AccountName.Contains(filter.AccountName))
            ).ToList();
        
        return Ok(account);
    }

    [HttpGet("/api/account/{accountNumber}")]

    public IActionResult GetById(int accountNumber)
    {
        var account = Accounts.FirstOrDefault(x => x.AccountId == accountNumber);
        if (account == null)
        {
            return NotFound();
            
        }
        return Ok(account);
    }

    [HttpPut("/api/account/{accountNumber}")]

    public IActionResult Update( int accountNumber,[FromBody] AccountDto accountDto)
    {
        var account = Accounts.FirstOrDefault(x => x.AccountId == accountNumber);

        if (account == null)
        {
            return NotFound();
        }
        account.AccountName = accountDto.AccountName;
        account.Address = accountDto.Address;
        account.phone = accountDto.phone;
        account.Email = accountDto.Email;
        
        return Ok("Account updated successfully");
        
    }


    [HttpDelete("/api/account/{AccountNumber}")]

    public IActionResult DeleteAccount(int AccountNumber)
    {
        var account = Accounts.FirstOrDefault(x => x.AccountId == AccountNumber);

        if (account == null)
        {
            return NotFound();
        }
        Accounts.Remove(account);
        return Ok("Account deleted sucessfully");
    }
}

public class AccountFilterDto
{

    public string AccountName { get; set; }
    public string Email { get; set; }
}
public class AccountDto
{
    public string AccountName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string phone { get; set; }
    
    
    
    
}

public class Account
{
    public string Email { get; set; }
    public string AccountName { get; set; }
    public string phone { get; set; }
    public string Address { get; set; }
    public int AccountId { get; set; }
}