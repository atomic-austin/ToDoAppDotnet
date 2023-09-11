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
    public async Task<ToDoItem> Get(string id)
    {
        return await _dataSaver.Get(id);
    }
    
    [HttpGet("todo/")]
    public async Task<IReadOnlyList<ToDoItem>> GetAll()
    {
        return await _dataSaver.GetAll();
    }
}

