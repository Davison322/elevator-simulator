namespace ElevatorApp;

class Program
{
    static void Main(string[] args)
    {
        Elevator elevator1 = new Elevator();
        Elevator elevator2 = new Elevator();
        FreightElevator freightElevator1 = new FreightElevator();
        ElevatorManager elevatorManager = new ElevatorManager(new List<Elevator>
        {
            elevator1,
            elevator2,
            freightElevator1
        });
        elevatorManager.GetStatus();
    }
}