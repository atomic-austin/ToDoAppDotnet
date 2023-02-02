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
        controller.GetAll();
        mockDataSaver.Received().GetAll();
        mockDataSaver.DidNotReceive().Create(new ToDoItem());
        mockDataSaver.DidNotReceive().Update(new ToDoItem());
        mockDataSaver.DidNotReceive().Delete("");
    }
}