namespace ElevatorApp;

public class Elevator
{
    private List<int> _stops;
    
    public int CurrentFloor { get; private set; }
    public ElevatorState State { get; private set; }
    public int MaxFloor { get; private set; }
    public int MinFloor { get; private set; }
    public IReadOnlyList<int> Stops => _stops;

    public Elevator(int startFloor = 1, int minFloor = 1, int maxFloor = 10)
    {
        if (minFloor >= maxFloor)
            throw new ArgumentException("Min floor must be less than max floor");

        if (startFloor < minFloor || startFloor > maxFloor)
        {
            throw new ArgumentOutOfRangeException(nameof(startFloor), "Start floor must be between min and max floor");
        }
        CurrentFloor = startFloor;
        State = ElevatorState.Idle;
        MaxFloor = maxFloor;
        MinFloor = minFloor;
        _stops = new List<int>();
    }

    public void AddStop(int floor) 
    {
        throw new NotImplementedException(); //under construction
    }
    
    public void SetOutOfService()
    {
        State = ElevatorState.OutOfService;
        _stops.Clear();
    }

    public void SetInService()
    {
        State = ElevatorState.Idle;
    }


    public override string ToString()
    {
        return $"Floor: {CurrentFloor} | State: {State} | Stops: [{string.Join(", ", Stops)}]";
    }
}