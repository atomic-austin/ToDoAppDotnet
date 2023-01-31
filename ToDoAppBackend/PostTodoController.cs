using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class PostTodoController : ControllerBase
{
    private readonly IDataSaver _dataSaver;

    public PostTodoController(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
    }
    
    [HttpPost("todo")]
    public ToDoItem Post(ToDoItemBase data)
    {
        var newToDo = _dataSaver.Create(data);

        return newToDo;
    }
}

