using ElevatorApp;

namespace ElevatorTests;

public class FreightElevatorTests
{
    private FreightElevator _elevator;
    [SetUp]
    public void Setup()
    {
        _elevator = new FreightElevator();
    }
    
    [Test]
    public void FreightElevator_OnCreation_ShouldHaveCorrectState()
    {
        Assert.That(_elevator.MaxWeight, Is.EqualTo(1000));
    }
    
    [Test]
    public void FreightElevator_WithMaxWeight_ShouldHaveCorrectState()
    {
        _elevator = new FreightElevator(1, 1, 10, 100);
        Assert.That(_elevator.MaxWeight, Is.EqualTo(100));
    }

    [Test]
    public void FreightElevator_WithInvalidWeight_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => new FreightElevator(1, 1, 10, 0));
    }
}