namespace ElevatorApp;

public interface IElevator
{
    int CurrentFloor { get; }
    ElevatorState State { get; }
    int MinFloor { get; }
    int MaxFloor { get; }
    IReadOnlySet<int> Stops { get; }
    void AddStop(int floor);
    void Move();
    void SetOutOfService();
    void SetInService();
}