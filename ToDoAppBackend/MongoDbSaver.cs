using AutoMapper;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ToDoAppBackend;

public class MongoDbSaver : IDataSaver
{
    private readonly IMongoDatabase _db;
    private readonly IMapper _mapper;
    
    public MongoDbSaver(IMapper mapper)
    {
        _mapper = mapper;
        
        var uri = Environment.GetEnvironmentVariable("MONGO_URI") ?? throw new Exception("Could not find Mongo DB URI");
        var client = new MongoClient(uri);

        var db = client.GetDatabase("ToDoDb");

        _db = db ?? throw new InvalidOperationException("Could not find or create the db");
    }

    private IMongoCollection<MongoDbToDoItem> GetToDoCollection()
    {
        return _db.GetCollection<MongoDbToDoItem>("todos");
    }

    private async Task<MongoDbToDoItem> ReplaceToDo(ToDoItem replacementToDo)
    {
        if (replacementToDo.Id == null)
        {
            throw new ArgumentException("Cannot replace a todo without an id");
        }
        
        ObjectId id;
        try
        {
            id = new ObjectId(replacementToDo.Id);
        }
        catch (FormatException e)
        {
            throw new ArgumentException("Invalid ID format.", e);
        }

        var mongoReplacementTodo = _mapper.Map<MongoDbToDoItem>(replacementToDo);
        
        var collection = _db.GetCollection<MongoDbToDoItem>("todos");
        var filter = Builders<MongoDbToDoItem>.Filter.Eq(doc => doc._id, mongoReplacementTodo._id);
        var result = await collection.ReplaceOneAsync(filter, mongoReplacementTodo);
        if (result.IsAcknowledged)
        {
            return mongoReplacementTodo;
        }
        throw new InvalidOperationException("Could not replace todo");
    }

    public async Task<ToDoItem> Get(string id)
    {
        var objectId = new ObjectId(id);
        var dbToDoItem = await GetToDoCollection()
            .Find(x => x._id == objectId)
            .FirstOrDefaultAsync();
        return _mapper.Map<ToDoItem>(dbToDoItem);
    }
    public async Task<IReadOnlyList<ToDoItem>> GetAll()
    {
        var mongoToDoItems = await GetToDoCollection()
            .FindAsync(x => true)
            .Result
            .ToListAsync();
        
        return _mapper.Map<IReadOnlyList<ToDoItem>>(mongoToDoItems);
    }

    public async Task<ToDoItem> Create(ToDoItem toDo)
    {
        var newToDo = _mapper.Map<MongoDbToDoItem>(toDo);
        await GetToDoCollection().InsertOneAsync(newToDo);

        return _mapper.Map<ToDoItem>(newToDo);
    }

    public async Task<ToDoItem> Update(ToDoItem toDo)
    {
        var replacedToDo = await ReplaceToDo(toDo);
        return _mapper.Map<ToDoItem>(replacedToDo);
    }

    public async Task<string> Delete(string id)
    {
        var deleteResult = await GetToDoCollection().DeleteOneAsync(x => x._id == new ObjectId(id));
        if (deleteResult.DeletedCount == 1)
        {
            return id;
        }
        throw new InvalidOperationException("Could not delete todo");
    }
}