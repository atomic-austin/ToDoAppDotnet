using Moq;
using ToDoAppBackend;

namespace ToDoAppBackendTests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void GetControllerTest()
    {
        var mockDataSaver = new Mock<IDataSaver>();
        var controller = new GetTodoController(mockDataSaver.Object);
        controller.Get();
        mockDataSaver.Verify(mock => mock.Get(null), Times.Once());
    }
}