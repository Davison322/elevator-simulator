namespace ElevatorApp;

public class ElevatorManager
{
    private readonly List<Elevator> _elevators;
    
    public IReadOnlyList<Elevator> Elevators => _elevators; 

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
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        bool IsCompatible(Elevator elevator)
        {
            if (elevator.State == ElevatorState.OutOfService) return false;
            if (request.RequiresFreight && !(elevator is FreightElevator)) return false;
            if (request.FromFloor < elevator.MinFloor || request.FromFloor > elevator.MaxFloor) return false;
            if (request.ToFloor < elevator.MinFloor || request.ToFloor > elevator.MaxFloor) return false;
            return true;
        }

        Elevator bestElevator = null;
        int bestDistance = int.MaxValue;

        // Part 1: find closest idle elevator
        foreach (var elevator in _elevators)
        {
            if (!IsCompatible(elevator)) continue;
            if (elevator.State == ElevatorState.Idle)
            {
                int distance = Math.Abs(elevator.CurrentFloor - request.FromFloor);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestElevator = elevator;
                }
            }
        }
        
        // Part 2: no idle found, find elevator moving in the right direction
        if (bestElevator == null)
        {
            foreach (var elevator in _elevators)
            {
                if (!IsCompatible(elevator)) continue;
            }

        }
        
        // Part 3: no compatible moving elevator, pick least busy one
        if (bestElevator == null)
        {
            foreach (var elevator in _elevators)
            {
                if (!IsCompatible(elevator)) continue;
            }
        }
    }

    public void SetOutOfService(int index)
    {
        if (index < 0 || index >= Elevators.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        _elevators[index].SetOutOfService();
    }

    public void SetInService(int index)
    {
        if (index < 0 || index >= Elevators.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        _elevators[index].SetInService();
    }

    public void MoveElevatorTo()
    {
        throw new NotImplementedException();
    }

    public void GetStatus()
    {
        for (int i = 0; i < _elevators.Count; i++)
        {
            Console.WriteLine($"Elevator {i + 1}: {_elevators[i]}");
        }
    }

}