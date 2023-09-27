using AutoMapper;

namespace ToDoAppBackend
{
    class FileSaverToDoToToDoItemProfile : Profile
    {
        public FileSaverToDoToToDoItemProfile()
        {
            CreateMap<FileSaverToDoItem, ToDoItem>()
                .ForMember(
                    toDo => toDo.Id,
                    opt => opt.MapFrom(fileSaverItem => fileSaverItem.id));

            CreateMap<ToDoItem, FileSaverToDoItem>()
                .ForMember(
                    fileSaverTodo => fileSaverTodo.id,
                    opt => opt.MapFrom(toDoItem => toDoItem.Id));
        }
    }
}
