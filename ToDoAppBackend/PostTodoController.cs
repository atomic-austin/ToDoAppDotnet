using System.Net;
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
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ToDoItem>> Post(ToDoItem data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var newToDo = await _dataSaver.Create(data);

        return newToDo;
    }
}

