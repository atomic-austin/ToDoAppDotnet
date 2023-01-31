using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class PutTodoController : ControllerBase
{
    private readonly IDataSaver _dataSaver;

    public PutTodoController(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
    }
    
    [HttpPut("todo")]
    public ToDoItem Put(ToDoItem data)
    {
        var updatedToDo = _dataSaver.Update(data);

        return updatedToDo;
    }
}

