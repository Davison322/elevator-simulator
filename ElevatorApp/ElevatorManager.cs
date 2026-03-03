namespace ElevatorApp;

public class ElevatorManager
{
    private readonly List<Elevator> _elevators;

    public ElevatorManager(List<Elevator> elevators)
    {
        if (elevators == null)
        {
            throw new ArgumentNullException(nameof(elevators));
        }
        if (elevators.Count == 0)
        {
            throw new ArgumentException("Elevators cannot be empty");
        }
        _elevators = elevators;
    }

    public void RequestElevator()
    {
        throw new NotImplementedException();
    }

    public void SetOutOfService()
    {
        throw new NotImplementedException();   
    }

    public void SetInService()
    {
        throw new NotImplementedException();
    }

    public void MoveElevatorTo()
    {
        throw new NotImplementedException();
    }

    public void GetStatus()
    {
        throw new NotImplementedException();
    }

}