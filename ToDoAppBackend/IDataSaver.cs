
namespace ToDoAppBackend;
public interface IDataSaver
{
    Task<IReadOnlyList<ToDoItem>> GetAll();
    Task<ToDoItem> Get(string id);
    Task<ToDoItem> Create(ToDoItem toDo);
    Task<ToDoItem> Update(ToDoItem toDo);
    Task<string> Delete(string id);
}