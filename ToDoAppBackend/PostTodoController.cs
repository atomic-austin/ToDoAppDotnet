using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class PostTodoController : ControllerBase
{
    private readonly FileSaver _fileSaver = new FileSaver();
    
    [HttpPost("todo")]
    public ToDoItem Post(ToDoItemBase data)
    {
        var newToDo = _fileSaver.Create(data);

        return newToDo;
    }
}

