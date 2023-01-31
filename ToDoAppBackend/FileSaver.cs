using System.Text;
using System.Text.Json;

namespace ToDoAppBackend;
public class FileSaver : IDataSaver
{
    private readonly string _path = Path.Combine(Environment.CurrentDirectory, @"data/");

    private FileInfo[] GetAllDataFiles()
    {
        var directory = new DirectoryInfo(_path);
        return directory.GetFiles();
    }
    private ToDoItem GetToDoFromFile(string fileName)
    {
        StreamReader sr = File.OpenText(fileName);
        var fileData = sr.ReadToEnd();
        sr.Close();
        var toDo = JsonSerializer.Deserialize<ToDoItem>(fileData);
        if (toDo == null)
        {
            throw new ArgumentNullException(fileName);
        }
        return toDo;
    }
    
    private void WriteToFile(ToDoItem toDoItem)
    {
        var file = new FileInfo(_path + toDoItem.id + ".json");
        FileStream fs = file.Exists ? file.Open(FileMode.Truncate) : file.Create();
        byte[] fileData = new UTF8Encoding(true).GetBytes(JsonSerializer.Serialize(toDoItem));
        fs.Write(fileData, 0, fileData.Length);
        fs.Close();
    }

    public List<ToDoItem> Get(string? id)
    {
        var files = GetAllDataFiles();

        if (id == null)
        {
            return files.Select(file => GetToDoFromFile(file.FullName)).ToList();
        }

        var file = files.First(item => item.Name == id + ".json");

        return new List<ToDoItem>()
        {
            GetToDoFromFile(file.FullName)
        };
    }

    public ToDoItem Create(ToDoItemBase data)
    {
        var files = GetAllDataFiles();
        
        var newId = files.Length;

        var newToDo = new ToDoItem()
        {
            id = newId.ToString(),
            Title = data.Title,
            Desc = data.Desc,
            Status = data.Status,
        };
        
        try
        {
            WriteToFile(newToDo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return newToDo;
    }

    public ToDoItem Update(ToDoItem toDoItem)
    {
        try
        {
            WriteToFile(toDoItem);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return toDoItem;
    }
}