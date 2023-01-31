using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class GetTodoController : ControllerBase
{
    private readonly FileSaver _fileSaver = new FileSaver();
    
    [HttpGet("todo/{id:int?}")]
    public IEnumerable<ToDoItem> Get(int? id = null)
    {
        return _fileSaver.Get(id == null ? null : id.ToString());
    }
}

