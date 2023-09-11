using MongoDB.Driver;
using MongoDB.Bson;

namespace ToDoAppBackend;

public class MongoDbToDoItem : ToDoItem
{
    public ObjectId _id { get; set; }
}
public class MongoDbSaver : IDataSaver
{
    private readonly IMongoDatabase _db;

    private IMongoCollection<MongoDbToDoItem> GetToDoCollection()
    {
        return _db.GetCollection<MongoDbToDoItem>("todos");
    }

    private async Task<MongoDbToDoItem> ReplaceToDo(ToDoItem replacementToDo)
    {
        if (replacementToDo.id == null)
        {
            throw new ArgumentException("Cannot replace a todo without an id");
        }
        
        ObjectId id;
        try
        {
            id = new ObjectId(replacementToDo.id);
        }
        catch (FormatException e)
        {
            throw new ArgumentException("Invalid ID format.", e);
        }

        var mongoReplacementTodo = new MongoDbToDoItem
        {
            _id = id,
            Title = replacementToDo.Title,
            Desc = replacementToDo.Desc,
            Status = replacementToDo.Status ?? ToDoItemStatus.ToDo,
        };
        var collection = _db.GetCollection<MongoDbToDoItem>("todos");
        var filter = Builders<MongoDbToDoItem>.Filter.Eq(doc => doc._id, mongoReplacementTodo._id);
        var result = await collection.ReplaceOneAsync(filter, mongoReplacementTodo);
        if (result.IsAcknowledged)
        {
            return mongoReplacementTodo;
        }
        throw new InvalidOperationException("Could not replace todo");
    }

    public MongoDbSaver()
    {
        var uri = Environment.GetEnvironmentVariable("MONGO_URI") ?? throw new Exception("Could not find Mongo DB URI");
        var client = new MongoClient(uri);

        var db = client.GetDatabase("ToDoDb");

        _db = db ?? throw new InvalidOperationException("Could not find or create the db");
    }

    public async Task<ToDoItem> Get(string id)
    {
        var objectId = new ObjectId(id);
        return await GetToDoCollection()
            .Find(x => x._id == objectId)
            .FirstOrDefaultAsync();
    }
    public async Task<IReadOnlyList<ToDoItem>> GetAll()
    {
        var mongoToDoItems = await GetToDoCollection()
            .FindAsync(x => true)
            .Result
            .ToListAsync();
        var listToReturn = new List<ToDoItem>();
        foreach (var mongoToDoItem in mongoToDoItems)
        {
            var returnToDo = new ToDoItem()
            {
                id = mongoToDoItem._id.ToString(),
                Title = mongoToDoItem.Title,
                Desc = mongoToDoItem.Desc,
                Status = ToDoItemStatus.ToDo
            };
            listToReturn.Add(returnToDo);
        }
        return listToReturn;
    }

    public async Task<ToDoItem> Create(ToDoItem toDo)
    {
        var newToDo = new MongoDbToDoItem()
        {
            Title = toDo.Title,
            Desc = toDo.Desc,
            Status = toDo.Status ?? ToDoItemStatus.ToDo,
        };
        await GetToDoCollection().InsertOneAsync(newToDo);

        var returnToDo = new ToDoItem()
        {
            id = newToDo._id.ToString(),
            Title = newToDo.Title,
            Desc = newToDo.Desc,
            Status = newToDo.Status,
        };
        return returnToDo;
    }

    public async Task<ToDoItem> Update(ToDoItem toDo)
    {
        var replacedToDo = await ReplaceToDo(toDo);
        return new ToDoItem
        {
            id = replacedToDo._id.ToString(),
            Title = replacedToDo.Title,
            Desc = replacedToDo.Desc,
            Status = replacedToDo.Status,
        };
    }

    public async Task<string> Delete(string id)
    {
        var deleteResult = await GetToDoCollection().DeleteOneAsync(x => x._id == new ObjectId(id));
        if (deleteResult.DeletedCount == 1)
        {
            return id;
        }
        else
        {
            throw new InvalidOperationException("Could not delete todo");
        }
    }
}