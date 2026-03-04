namespace ElevatorApp;

public class FloorRequest
{
    public int FromFloor { get; }
    public int ToFloor { get; }
    
    public bool RequiresFreight { get; }
    
    public FloorRequest(int fromFloor, int toFloor, bool requiresFreight = false)
    {
        if (fromFloor == toFloor)
        {
            throw new ArgumentException("From and to floor cannot be the same");
        }
        FromFloor = fromFloor;
        ToFloor = toFloor;
        RequiresFreight = requiresFreight;
    }
}