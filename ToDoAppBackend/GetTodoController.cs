using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class GetTodoController : ControllerBase
{
    private readonly IDataSaver _dataSaver;

    public GetTodoController(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
    }
    
    [HttpGet("todo/{id}")]
    public ToDoItem Get(string id)
    {
        return _dataSaver.Get(id);
    }
    
    [HttpGet("todo/")]
    public IReadOnlyList<ToDoItem> GetAll()
    {
        return _dataSaver.GetAll();
    }
}

