using NSubstitute;
using ToDoAppBackend;

namespace ToDoApiTests;

public class ApiTests
{
    [Fact]
    public void GetControllerTest()
    {
        var mockDataSaver = Substitute.For<IDataSaver>();
        var controller = new GetTodoController(mockDataSaver);
        controller.Get();
        mockDataSaver.Received().Get(null);
        mockDataSaver.DidNotReceive().Create(new ToDoItemBase());
        mockDataSaver.DidNotReceive().Update(new ToDoItem());
        mockDataSaver.DidNotReceive().Delete("");
    }
}