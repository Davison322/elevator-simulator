namespace ElevatorApp;

public class ElevatorManager
{
    private readonly List<Elevator> _elevators;
    private List<FloorRequest> _pendingRequests = new List<FloorRequest>();
    
    public IReadOnlyList<Elevator> Elevators => _elevators; 
    public IReadOnlyList<FloorRequest> PendingRequests => _pendingRequests;

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
    
    public void RequestElevator(FloorRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (!IsRequestPossible(request))
            throw new InvalidOperationException("No elevator can serve this request");

        var idle = GetClosestIdleElevator(request);

        if (idle != null)
        {
            idle.AddStop(request.FromFloor);
            idle.AddStop(request.ToFloor);
        }
        else
        {
            _pendingRequests.Add(request);
        }
    }

    public void Tick()
    {
        foreach (var elevator in _elevators)
            elevator.Move();

        for (int i = 0; i < _pendingRequests.Count; i++)
        {
            var request = _pendingRequests[i];
            var capable = GetClosestIdleElevator(request);

            if (capable != null)
            {
                capable.AddStop(request.FromFloor);
                capable.AddStop(request.ToFloor);
                _pendingRequests.RemoveAt(i);
                i--;
            }
        }
    }

    private bool IsRequestPossible(FloorRequest request)
    {
        return _elevators.Any(e =>
            e.State != ElevatorState.OutOfService &&
            e.MinFloor <= request.FromFloor && e.MaxFloor >= request.FromFloor &&
            e.MinFloor <= request.ToFloor && e.MaxFloor >= request.ToFloor &&
            (!request.RequiresFreight || e is FreightElevator));
    }

    private Elevator? GetClosestIdleElevator(FloorRequest request)
    {
        return _elevators.Where(e =>
                e.State == ElevatorState.Idle &&
                e.MinFloor <= request.FromFloor && e.MaxFloor >= request.FromFloor &&
                e.MinFloor <= request.ToFloor && e.MaxFloor >= request.ToFloor &&
                (!request.RequiresFreight || e is FreightElevator))
            .MinBy(e => Math.Abs(e.CurrentFloor - request.FromFloor));
    }

    public void SetOutOfService(int index)
    {
        if (index < 0 || index >= Elevators.Count)
            throw new ArgumentOutOfRangeException(nameof(index));
            
        _elevators[index].SetOutOfService();
    }

    public void SetInService(int index)
    {
        if (index < 0 || index >= Elevators.Count)
            throw new ArgumentOutOfRangeException(nameof(index));
            
        _elevators[index].SetInService();
    }
    
    public void GetStatus() //maybe should be return(debatable)
    {
        for (int i = 0; i < _elevators.Count; i++)
        {
            Console.WriteLine($"Elevator {i + 1}: {_elevators[i]}");
        }
    }
}