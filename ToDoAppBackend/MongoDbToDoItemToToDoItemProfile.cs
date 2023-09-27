using AutoMapper;

namespace ToDoAppBackend
{
    class MongoDbToDoToToDoItemProfile : Profile
    {
        public MongoDbToDoToToDoItemProfile()
        {
            CreateMap<MongoDbToDoItem, ToDoItem>()
                .ForMember(
                    toDo => toDo.Id,
                    opt => opt.MapFrom(dbItem => dbItem._id));
        }
    }
}
