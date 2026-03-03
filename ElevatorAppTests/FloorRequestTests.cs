using ElevatorApp;

namespace ElevatorTests;

public class FloorRequestTests
{
    private FloorRequest _request;

    [SetUp]
    public void Setup()
    {
        _request = new FloorRequest(1, 2);
    }

    [Test]
    public void FloorRequest_WithSameFloor_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => new FloorRequest(1, 1));
    }
    
    [Test]
    public void FloorRequest_OnCreation_ShouldStoreFromFloor()
    {
        Assert.That(_request.FromFloor, Is.EqualTo(1));
    }

    [Test]
    public void FloorRequest_OnCreation_ShouldStoreToFloor()
    {
        Assert.That(_request.ToFloor, Is.EqualTo(2));
    }
}