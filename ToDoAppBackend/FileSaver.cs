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
    private static async Task<ToDoItem> GetToDoFromFile(string fileName)
    {
        using var sr = new StreamReader(fileName);
        var fileData = await sr.ReadToEndAsync();
        var toDo = JsonSerializer.Deserialize<ToDoItem>(fileData);
        if (toDo == null)
        {
            throw new ArgumentNullException(nameof(fileName), "Deserialized object is null.");
        }
        return toDo;
    }

    
    private async Task WriteToFile(ToDoItem toDoItem)
    {
        var file = new FileInfo(_path + toDoItem.id + ".json");
        FileStream fs = file.Exists ? file.Open(FileMode.Truncate) : file.Create();
        byte[] fileData = new UTF8Encoding(true).GetBytes(JsonSerializer.Serialize(toDoItem));
        await fs.WriteAsync(fileData, 0, fileData.Length);
        fs.Close();
    }

    public async Task<ToDoItem> Get(string id)
    {
        var files = GetAllDataFiles();

        var file = files.First(item => item.Name == id + ".json");

        return await GetToDoFromFile(file.FullName);
    }

    public async Task<IReadOnlyList<ToDoItem>> GetAll()
    {
        var files = GetAllDataFiles();
        var tasks = files.Select(async file => await GetToDoFromFile(file.FullName));
        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    public async Task<ToDoItem> Create(ToDoItem data)
    {
        if (data.id != null)
        {
            throw new ArgumentException("Id must be null when creating a new todo");
        }
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
            await WriteToFile(newToDo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return newToDo;
    }

    public async Task<ToDoItem> Update(ToDoItem toDoItem)
    {
        try
        {
            await WriteToFile(toDoItem);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return toDoItem;
    }

    public async Task<string> Delete(string id)
    {
        var path = _path + id + ".json";
        try
        {
            await Task.Run(() => File.Delete(path));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return id;
    }
}