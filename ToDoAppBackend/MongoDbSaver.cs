using MongoDB.Driver;
using MongoDB.Bson;

namespace ToDoAppBackend;

public class MongoDbToDoItem : ToDoItemData
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
            throw new Exception("Cannot replace a todo without an id");
        }

        var mongoReplacementTodo = new MongoDbToDoItem
        {
            _id = new ObjectId(replacementToDo.id),
            Title = replacementToDo.Title,
            Desc = replacementToDo.Desc,
            Status = replacementToDo.Status,
        };
        var collection = _db.GetCollection<MongoDbToDoItem>("todos");
        var filter = Builders<MongoDbToDoItem>.Filter.Eq(doc => doc._id, mongoReplacementTodo._id);
        var result = await collection.ReplaceOneAsync(filter, mongoReplacementTodo);
        if (result.IsAcknowledged)
        {
            return mongoReplacementTodo;
        }
        throw new Exception("Could not replace todo");
    }

    public MongoDbSaver()
    {
        var uri = Environment.GetEnvironmentVariable("MONGO_URI") ?? throw new Exception("Could not find Mongo DB URI");
        var client = new MongoClient(uri);

        var db = client.GetDatabase("ToDoDb");

        _db = db ?? throw new Exception("Could not find or create the db");
    }

    public ToDoItem Get(string id)
    {
        return new ToDoItem();
    }
    public IReadOnlyList<ToDoItem> GetAll()
    {
        var mongoToDoItems = GetToDoCollection().Find(_ => true).ToList();
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

    public ToDoItem Create(ToDoItem toDo)
    {
        var newToDo = new MongoDbToDoItem()
        {
            Title = toDo.Title,
            Desc = "blah",
            Status = 0
        };
        GetToDoCollection().InsertOne(newToDo);

        var returnToDo = new ToDoItem()
        {
            id = newToDo._id.ToString(),
            Title = newToDo.Title,
            Desc = newToDo.Desc,
            Status = ToDoItemStatus.ToDo,
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

    public string Delete(string id)
    {
        return "TBD";
    }
}