
namespace ToDoAppBackend;
interface IDataSaver
{
    List<ToDoItem> Get(string? id);
    ToDoItem Create(ToDoItemBase toDo);
    ToDoItem Update(ToDoItem toDo);
    string Delete(string id);
}