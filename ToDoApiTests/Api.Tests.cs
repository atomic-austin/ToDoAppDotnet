using NSubstitute;
using NUnit.Framework;
using ToDoAppBackend;

namespace ToDoApiTests;

[TestFixture]
public class ApiTests
{
    [Test]
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