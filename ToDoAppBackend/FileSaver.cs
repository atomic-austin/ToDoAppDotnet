using System.Text;
using System.Text.Json;
using AutoMapper;

namespace ToDoAppBackend;
public class FileSaver : IDataSaver
{
    private readonly IMapper _mapper;
    private readonly string _path = Path.Combine(Environment.CurrentDirectory, @"data/");

    public FileSaver(IMapper mapper)
    {
        _mapper = mapper;
        CreateDirectoryIfNotExists();
    }
    
    private void CreateDirectoryIfNotExists()
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }
    private FileInfo[] GetAllDataFiles()
    {
        var directory = new DirectoryInfo(_path);
        return directory.GetFiles();
    }
    private static async Task<FileSaverToDoItem> GetToDoFromFile(string fileName)
    {
        using var sr = new StreamReader(fileName);
        var fileData = await sr.ReadToEndAsync();
        var toDo = JsonSerializer.Deserialize<FileSaverToDoItem>(fileData);
        if (toDo == null)
        {
            throw new ArgumentNullException(nameof(fileName), "Deserialized object is null.");
        }
        return toDo;
    }
    
    private async Task WriteToFile(ToDoItem toDoItem)
    {
        var toDo = _mapper.Map<FileSaverToDoItem>(toDoItem);
        var file = new FileInfo(_path + toDo.id + ".json");
        FileStream fs = file.Exists ? file.Open(FileMode.Truncate) : file.Create();
        byte[] fileData = new UTF8Encoding(true).GetBytes(JsonSerializer.Serialize(toDo));
        await fs.WriteAsync(fileData, 0, fileData.Length);
        fs.Close();
    }

    public async Task<ToDoItem> Get(string id)
    {
        var files = GetAllDataFiles();

        var file = files.First(item => item.Name == id + ".json");
        var toDo = await GetToDoFromFile(file.FullName);
        return _mapper.Map<ToDoItem>(toDo);
    }

    public async Task<IReadOnlyList<ToDoItem>> GetAll()
    {
        var files = GetAllDataFiles();
        var tasks = files.Select(async file => await GetToDoFromFile(file.FullName));
        var results = await Task.WhenAll(tasks);
        return _mapper.Map<IReadOnlyList<ToDoItem>>(results);
    }

    public async Task<ToDoItem> Create(ToDoItem newToDoData)
    {
        if (newToDoData.Id != null)
        {
            throw new ArgumentException("Id must be null when creating a new todo");
        }
        var files = GetAllDataFiles();
        
        var newId = files.Length;

        newToDoData.Id ??= newId.ToString();
        
        try
        {
            await WriteToFile(newToDoData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new FileSaverException("Error while writing to file", e);
        }

        return newToDoData;
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
            throw new FileSaverException("Error while updating file", e);
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
            throw new FileSaverException("Error while deleting file", e);
        }

        return id;
    }
}