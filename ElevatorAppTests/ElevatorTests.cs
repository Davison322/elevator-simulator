using ElevatorApp;

namespace ElevatorTests;

public class ElevatorTests
{
    private Elevator _elevator;
    
    [SetUp]
    public void Setup()
    {
        _elevator = new Elevator();
    }

    [Test]
    public void Elevator_OnCreation_ShouldHaveEmptyStops()
    {
        Assert.That(_elevator.Stops, Is.Empty);
    }
    
    [Test]
    public void Elevator_WithStartFloor_ShouldHaveCorrectFloor()
    {
        _elevator = new Elevator(2);
        Assert.That(_elevator.CurrentFloor, Is.EqualTo(2));   
    }

    [Test]
    public void Elevator_WithCustomFloorRange_ShouldHaveCorrectFloor()
    {
        _elevator = new Elevator(15, -5, 36);
        Assert.That(_elevator.CurrentFloor, Is.EqualTo(15));  
    }

    [Test]
    public void Elevator_WithInvalidStartFloor_ShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Elevator(11));
    }

    [Test]
    public void Elevator_WithMinGreaterThanMax_ShouldThrow()
    {
        Assert.Throws<ArgumentException>((() => new Elevator(1, 3, 2)));
    }
    
    [Test]
    public void AddStop_WithInvalidFloor_ShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _elevator.AddStop(11));
    }
    
    [Test]
    public void AddStop_WithValidFloor_ShouldAppearInStops()
    {
        _elevator.AddStop(2);
        _elevator.AddStop(3);
        Assert.That(_elevator.Stops, Is.EquivalentTo(new []{2,3}));
    }
    
    [Test]
    public void AddStop_WhenIdle_ShouldSetStateToMovingUp()
    {
        _elevator.AddStop(5);
        Assert.That(_elevator.State, Is.EqualTo(ElevatorState.MovingUp));
    }

    [Test]
    public void AddStop_WhenIdle_ShouldSetStateToMovingDown()
    {
        Elevator elevator = new Elevator(5, 1, 10);
        elevator.AddStop(2);
        Assert.That(elevator.State, Is.EqualTo(ElevatorState.MovingDown));
    }

    [Test]
    public void SetOutOfService_ShouldSetStateToOutOfService()
    {
        _elevator.SetOutOfService();
        Assert.That(_elevator.State, Is.EqualTo(ElevatorState.OutOfService));
    }
    
    [Test]
    public void SetOutOfService_ShouldClearStops()
    {
        _elevator.AddStop(2);
        _elevator.AddStop(3);
        _elevator.SetOutOfService();
        Assert.That(_elevator.Stops, Is.Empty);
    }
    
    [Test]
    public void SetInService_ShouldSetStateToIdle()
    {
        _elevator.SetInService();
        Assert.That(_elevator.State, Is.EqualTo(ElevatorState.Idle));
    }
    
    [Test]
    public void Move_CurrentFloorShouldMoveUp()
    {
        _elevator.AddStop(5);
        _elevator.Move();
        Assert.That(_elevator.CurrentFloor, Is.EqualTo(2));    }
    
    [Test]
    public void Move_CurrentFloorShouldStay()
    {
        _elevator.Move();
        Assert.That(_elevator.CurrentFloor, Is.EqualTo(1));
    }

    [Test]
    public void Move_ShouldSetStateToMovingDown()
    {
        _elevator = new Elevator(10);
        _elevator.AddStop(5);
        _elevator.Move();
        Assert.That(_elevator.State, Is.EqualTo(ElevatorState.MovingDown));
    }
    
    [Test]
    public void Move_ShouldClearStops()
    {
        _elevator.AddStop(2);
        _elevator.Move();
        Assert.That(_elevator.Stops, Is.Empty);
    }
    
    [Test]
    public void Move_ShouldSetStateToIdle()
    {
        _elevator.AddStop(2);
        _elevator.Move();
        Assert.That(_elevator.State, Is.EqualTo(ElevatorState.Idle));
    }
    
    [Test]
    public void Move_WithMultipleStops_ShouldNotBeIdle()
    {
        _elevator.AddStop(3);
        _elevator.AddStop(5);
        _elevator.Move();
        Assert.That(_elevator.State, Is.Not.EqualTo(ElevatorState.Idle));
    }

    
}