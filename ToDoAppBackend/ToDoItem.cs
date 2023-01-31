namespace ToDoAppBackend;

public enum ToDoItemStatus
{
    ToDo = 0,
    InProgress = 1,
    Done = 2,
}

public interface IToDoItemBase
{
    string Title { get; set; }
    string Desc  { get; set; }
    ToDoItemStatus Status  { get; set; }
}

public interface ITodoItem
{
    string id { get; set; }
}

public class ToDoItemBase : IToDoItemBase
{
    public string Title { get; set; }
    public string Desc { get; set; }
    public ToDoItemStatus Status { get; set; }
}

public class ToDoItem : ITodoItem, IToDoItemBase
{
    public string id { get; set; }
    public string Title { get; set; }
    public string Desc { get; set; }
    public ToDoItemStatus Status { get; set; }
}