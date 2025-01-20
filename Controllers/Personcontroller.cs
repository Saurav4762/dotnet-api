using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Controllers;

public class Personcontroller : ControllerBase 
{
    [HttpGet("greet")]

    public IActionResult Greet() 
    {
        return Ok ("Hello world : ");

    }

    [HttpGet ("greet/{name}/{age}")]
    
    public IActionResult Greet(string name, int age)
    {
        return Ok ($"Hello, {name}! You are {age} years old");

    }
}