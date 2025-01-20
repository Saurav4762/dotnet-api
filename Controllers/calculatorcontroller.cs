using Microsoft.AspNetCore.Mvc;

namespace Dotnet .Controllers;

public class Calculatorcontroller : ControllerBase
{
    [HttpGet("add")]

    public IActionResult Add ([FromQuery] int a, [FromQuery] int b)
      {
        return Ok ($"THE SUM IS : {a + b}");
      }

      [HttpGet("subtract")]

      public IActionResult Subtract([FromQuery] int a, [FromQuery] int b)
      {
        return Ok ($" The subtraction is : {a-b}");
      }
    



}