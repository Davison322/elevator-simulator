namespace ElevatorApp;

class Program
{
    static void Main()
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
        while (true)
        {
            try
            {
                Console.WriteLine("\nElevator simulator. Select an option: ");
                Console.WriteLine("1. Request elevator");
                Console.WriteLine("2. Tick(advance simulation)");
                Console.WriteLine("3. Get status");
                Console.WriteLine("4. Set elevator out of service");
                Console.WriteLine("5. Set elevator in service");
                Console.WriteLine("6. Exit");
                
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Console.WriteLine("Enter from floor:");
                        int fromFloor = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter to floor:");
                        int toFloor = int.Parse(Console.ReadLine());
                        Console.WriteLine("Requires freight? (y/n):");
                        bool requiresFreight = Console.ReadLine() == "y";
                        elevatorManager.RequestElevator(new FloorRequest(fromFloor, toFloor, requiresFreight));
                        Console.WriteLine("Request accepted!");
                        break;
                    case 2:
                        elevatorManager.Tick();
                        Console.WriteLine("Tick advanced!");
                        break;
                    case 3:
                        elevatorManager.GetStatus();
                        break;
                    case 4:
                        Console.WriteLine("Enter elevator index (0, 1, 2):");
                        int outIndex = int.Parse(Console.ReadLine());
                        elevatorManager.SetOutOfService(outIndex);
                        Console.WriteLine("Elevator set out of service!");
                        break;
                    case 5:
                        Console.WriteLine("Enter elevator index (0, 1, 2):");
                        int inIndex = int.Parse(Console.ReadLine());
                        elevatorManager.SetInService(inIndex);
                        Console.WriteLine("Elevator set in service!");
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}