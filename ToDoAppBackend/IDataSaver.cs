
namespace ToDoAppBackend;
public interface IDataSaver
{
    IReadOnlyList<ToDoItem> GetAll();
    ToDoItem Get(string id);
    ToDoItem Create(ToDoItem toDo);
    Task<ToDoItem> Update(ToDoItem toDo);
    string Delete(string id);
}