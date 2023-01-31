using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class PutTodoController : ControllerBase
{
    private readonly FileSaver _fileSaver = new FileSaver();
    
    [HttpPut("todo")]
    public ToDoItem Put(ToDoItem data)
    {
        var updatedToDo = _fileSaver.Update(data);

        return updatedToDo;
    }
}

