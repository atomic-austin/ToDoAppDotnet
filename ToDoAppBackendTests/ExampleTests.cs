namespace ToDoAppBackendTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ExampleTest1()
    {
        Assert.Pass();
    }
    
    [Test]
    public void ExampleTest2()
    {
        Assert.That(1 < 2, Is.True);
    }
}