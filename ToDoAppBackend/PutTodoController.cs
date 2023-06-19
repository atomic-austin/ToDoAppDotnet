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
    public async Task<ToDoItem> Put(ToDoItem data)
    {
        var updatedToDo = await _dataSaver.Update(data);

        return updatedToDo;
    }
}

