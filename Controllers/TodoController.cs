using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

public class TodoController:ControllerBase
{
    public static List<Task> tasks = new List<Task>();

    [HttpPost("/api/tasks")]

    public IActionResult CreateTask([FromForm] TaskDto taskDto)
    {
        var newtask = new Task
        {
            id = tasks.Count + 1,
            title = taskDto.title,
            description = taskDto.description,
            Iscompleted = false
        };
        
        tasks.Add(newtask);
        return CreatedAtAction(nameof(GetById), new { id = newtask.id }, tasks);
    }

    [HttpGet("/api/tasks")]

    public IActionResult GetTasks([FromQuery] TaskDtoFilter filter)
    {
        var task = tasks.Where(X =>
            (string.IsNullOrEmpty(filter.title) || X.title.Contains(filter.title))
        ).ToList();
        
        return Ok(tasks);
    }

    [HttpGet("/api/tasks/{id}")]

    public IActionResult GetById(int id)
    {
        var task = tasks.FirstOrDefault(x => x.id == id);
        if (task == null)
        {
            return NotFound("Please enter valid Id ");
        }
        return Ok(task);
    }

    [HttpPut("/api/tasks/{id}")]

    public IActionResult UpdateTask(int id, [FromBody] TaskDto taskDto)

    {
        var task = tasks.FirstOrDefault(x => x.id == id);

        if (task == null)
        {
            return NotFound("Please enter valid Id ");
        }
        task.title = taskDto.title;
        task.description = taskDto.description;
        task.Iscompleted = taskDto.Iscompleted;
        return Ok(task);
    }

    [HttpDelete("/api/tasks/{id}")]
    
    public IActionResult DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(x => x.id == id);
        if (task == null)
        {
            return NotFound("Please enter valid Id ");
        }
        tasks.Remove(task);
        return Ok("Deleted sucessfully");
    }
    
}

public class TaskDtoFilter
{
    public string title { get; set; }
   
}

public class TaskDto
{
    public string title { get; set; }
    public string description { get; set; }
    public bool Iscompleted { get; set; }
}
public class Task
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public bool Iscompleted { get; set; }
}