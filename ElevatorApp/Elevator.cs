namespace ElevatorApp;

public class Elevator
{
    private HashSet<int> _stops;
    
    public int CurrentFloor { get; private set; }
    public ElevatorState State { get; private set; }
    public int MaxFloor { get; private set; }
    public int MinFloor { get; private set; }
    public IReadOnlySet<int> Stops => _stops;

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
        _stops = new HashSet<int>();
    }

    public void AddStop(int floor) 
    {
        if (State == ElevatorState.OutOfService)
            throw new InvalidOperationException("Elevator is out of service.");

        if (floor < MinFloor || floor > MaxFloor)
            throw new ArgumentOutOfRangeException(nameof(floor));

        if (floor == CurrentFloor && State == ElevatorState.Idle)
            return;
        
        _stops.Add(floor);
        
        if (State == ElevatorState.Idle)
        {
            State = floor > CurrentFloor ? ElevatorState.MovingUp : ElevatorState.MovingDown;
        }
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

    public void Move()
    {
        if (State == ElevatorState.OutOfService || State == ElevatorState.Idle)
        {
            return;
        }

        if (State == ElevatorState.MovingUp)
        {
            if (CurrentFloor >= MaxFloor)
                throw new InvalidOperationException("Elevator exceeded max floor");
            CurrentFloor++;
        }
        else if (State == ElevatorState.MovingDown)
        {
            if (CurrentFloor <= MinFloor)
                throw new InvalidOperationException("Elevator surpass min floor");
            CurrentFloor--;
        }

        if (_stops.Contains(CurrentFloor))
        {
            _stops.Remove(CurrentFloor);
        }

        if (_stops.Count == 0)
        {
            State = ElevatorState.Idle;
        }
        else if (State == ElevatorState.MovingUp && !_stops.Any(f => f > CurrentFloor))
        {
            State = ElevatorState.MovingDown;
        }
        else if (State == ElevatorState.MovingDown && !_stops.Any(f => f < CurrentFloor))
        {
            State = ElevatorState.MovingUp;
        }
    }
    public override string ToString()
    {
        return $"Floor: {CurrentFloor} | State: {State} | Stops: [{string.Join(", ", Stops)}]";
    }
}