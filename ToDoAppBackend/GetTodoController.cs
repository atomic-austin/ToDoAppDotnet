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
    
    [HttpGet("todo/{id:int?}")]
    public IEnumerable<ToDoItem> Get(int? id = null)
    {
        return _dataSaver.Get(id == null ? null : id.ToString());
    }
}

