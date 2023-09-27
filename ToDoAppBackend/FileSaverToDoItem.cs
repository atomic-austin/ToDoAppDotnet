namespace ToDoAppBackend;

public class FileSaverToDoItem
{
    public string? id { get; set; }
    public string title { get; set; }
    public string desc { get; set; }
    public ToDoItemStatus? status { get; set; }
}