namespace ElevatorApp;

public class FloorRequest
{
    public int FromFloor { get; }
    public int ToFloor { get; }
    
    public FloorRequest(int fromFloor, int toFloor)
    {
        if (fromFloor == toFloor)
        {
            throw new ArgumentException("From and to floor cannot be the same");
        }
        FromFloor = fromFloor;
        ToFloor = toFloor;
    }
}