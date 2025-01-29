using Dotnet.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

public class TodoAssigncontollers : ControllerBase
{
    public static List<TodoAssign> TodoAssigns = new List<TodoAssign>();

    public static List<Person> Persons = new List<Person>();

    public static List<Todo> Todos = new List<Todo>();

    [HttpPost("/api/todo-assign")]
    public IActionResult Assigns ([FromBody] TodoAssignDto todoAssign)
    {
        var assigned = new TodoAssign()
        {
            Id = todoAssign.Count + 1,
            TodoId = todoAssign.TodoId,
            PersonId =  todoAssign.personId
        };
        TodoAssigns.Add(assigned);
        return Ok(assigned);
    }

    [HttpPost("/api/person-assign")]
    public IActionResult create([FromForm] PersonDto person)
    {
        var assigned = new Person()
        {
            Id = person.Id,
            Name = person.Name
        };
        Persons.Add(assigned);
        return Ok(assigned);
    }

    [HttpGet("/api/todo-assign/{todoId}")]
    public IActionResult GetAssigns(int todoId)
    {
        // var assign = store.TodoAssigns.FirstOrDefault(x => x.TodoId == todoId);
        //
        // var person = store.Persons.FirstOrDefault(x => x.Id == assign.PersonId);
        // var todo = store.Todos.FirstOrDefault(x => x.Id == todoId);

        var result = (from a in TodoAssigns
            join t in Todos on a.TodoId equals t.Id
            join p in Persons on a.PersonId equals p.Id
            where a.TodoId == todoId
            select new
            {
                Todo = t,
                Person = p,
            }).ToList();
            

        // return Ok(new
        // {
        //     Todo = todo,
        //     Person = person
        // });
    }

}
public class TodoAssignDto
{
    public int TodoId { get; set; }
    public int personId { get; set; }
    
}
public class TodoAssign
{
    public int TodoId { get; set; }
    public int PersonId { get; set; }
    public int Id { get; set; }
}
public class PersonDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class Todo
{
    public int Id { get; set; }
    public string Name { get; set; }
    //public Todostatus status { get; set; }
}

