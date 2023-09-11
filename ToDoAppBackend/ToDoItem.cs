namespace ToDoAppBackend;

public enum ToDoItemStatus
{
    ToDo = 0,
    InProgress = 1,
    Done = 2,
}

public class ToDoItem
{
    public string? id { get; set; }
    public string Title { get; set; }
    public string Desc { get; set; }
    public ToDoItemStatus? Status { get; set; }
}