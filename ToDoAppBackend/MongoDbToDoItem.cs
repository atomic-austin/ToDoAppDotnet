using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ToDoAppBackend;

public class MongoDbToDoItem
{
    [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
    public ObjectId? _id { get; set; }
    public string Title { get; set; }
    public string Desc { get; set; }
    public ToDoItemStatus? Status { get; set; }
}