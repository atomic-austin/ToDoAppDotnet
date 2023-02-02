
namespace ToDoAppBackend;
public interface IDataSaver
{
    IReadOnlyList<ToDoItem> GetAll();
    ToDoItem Get(string id);
    ToDoItem Create(ToDoItem toDo);
    ToDoItem Update(ToDoItem toDo);
    string Delete(string id);
}