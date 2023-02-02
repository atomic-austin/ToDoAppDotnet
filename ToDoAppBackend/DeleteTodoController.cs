using Microsoft.AspNetCore.Mvc;

namespace ToDoAppBackend;

[ApiController]
public class DeleteTodoController : ControllerBase
{
    private readonly IDataSaver _dataSaver;

    public DeleteTodoController(IDataSaver dataSaver)
    {
        _dataSaver = dataSaver;
    }
    
    [HttpDelete("todo/{id}")]
    public string Delete(string id)
    {
        return _dataSaver.Delete(id);
    }
}

