namespace ElevatorApp;

public class FreightElevator : Elevator
{
    public int MaxWeight { get; private set; }
    
    public FreightElevator(int startFloor = 1, int minFloor = 1, int maxFloor = 10, int maxWeight = 1000) : base(startFloor, minFloor, maxFloor)
    {
        if (maxWeight <= 0)
        {
            throw new ArgumentException("Max weight must be greater than 0");
        }
        MaxWeight = maxWeight;
    }
}