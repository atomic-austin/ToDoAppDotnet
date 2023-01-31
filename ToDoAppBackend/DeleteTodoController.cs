using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class DeleteTodoController : ControllerBase
{
    private readonly FileSaver _fileSaver = new FileSaver();
    
    [HttpDelete("todo/{id:int}")]
    public string Delete(int id)
    {
        return _fileSaver.Delete(id.ToString());
    }
}

