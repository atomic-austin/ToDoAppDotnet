namespace ToDoAppBackend;

public enum ToDoItemStatus
{
    ToDo = 0,
    InProgress = 1,
    Done = 2,
}

public class ToDoItemData
{
    public string Title { get; set; }
    public string Desc { get; set; }
    public ToDoItemStatus Status { get; set; }
}

public class ToDoItem : ToDoItemData
{
    public string? id { get; set; } = null;
    public string Title { get; set; }
    public string Desc { get; set; }
    public ToDoItemStatus Status { get; set; }
}