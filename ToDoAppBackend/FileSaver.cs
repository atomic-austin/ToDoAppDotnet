using System.Text;
using System.Text.Json;

namespace ToDoAppBackend;
public class FileSaver : IDataSaver
{
    private readonly string _path = Path.Combine(Environment.CurrentDirectory, @"data/");
    
    public List<ToDoItem> Get()
    {
        var directory = new DirectoryInfo(_path);
        var files = directory.GetFiles();
    
        var toDos = new List<ToDoItem>();
    
        foreach (var file in files)
        {
            StreamReader sr = File.OpenText(file.FullName);
            var fileData = sr.ReadToEnd();
            var deserialised = JsonSerializer.Deserialize<ToDoItem>(fileData);
            toDos.Add(deserialised);
        }
    
        return toDos;
    }
    
    public List<ToDoItem> Get(string? id)
    {
        var directory = new DirectoryInfo(_path);
        var files = directory.GetFiles();

        var file = files.First(item => item.Name == id + ".json");

        StreamReader sr = File.OpenText(file.FullName);
        var fileData = sr.ReadToEnd();
        var deserialised = JsonSerializer.Deserialize<ToDoItem>(fileData);
        var dataToReturn = new List<ToDoItem>();
        dataToReturn.Add(deserialised);
        return dataToReturn;
    }

    public ToDoItem Create(ToDoItemBase data)
    {
        var directory = new DirectoryInfo(_path);
        var files = directory.GetFiles();
        
        var newId = files.Length;
        
        var file = new FileInfo(_path + newId + ".json");
        if (file.Exists)
        {
            throw new Exception("Already exists");
        }

        var newToDo = new ToDoItem()
        {
            id = newId.ToString(),
            Title = data.Title,
            Desc = data.Desc,
            Status = data.Status,
        };
        
        FileStream fs = file.Create();
        byte[] fileData = new UTF8Encoding(true).GetBytes(JsonSerializer.Serialize(newToDo));
        fs.Write(fileData, 0, fileData.Length);
        fs.Close();

        return newToDo;
    }
}