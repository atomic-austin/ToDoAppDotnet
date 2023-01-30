using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class GetTodoController : ControllerBase
{
    private readonly FileSaver _fileSaver = new FileSaver();
    
    [HttpGet("todo/{id:int?}")]
    public IEnumerable<ToDoItem> Get(int? id)
    {
        var data = id != null ? _fileSaver.Get(id.ToString()) : _fileSaver.Get();

        return data;
    }
}

