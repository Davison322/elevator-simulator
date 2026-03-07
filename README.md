# Smart Elevator Simulator

A C# console application simulating a smart elevator dispatch system,
inspired by modern destination dispatch systems.

## About

The system manages multiple elevators in a building, assigning
requests to the most suitable elevator based on proximity and availability.
Passengers specify their origin and destination floor — the manager decides
which elevator to send.

## Features

- Multiple elevator support (standard and freight)
- Smart dispatch — closest idle elevator gets the request
- FIFO pending queue for when all elevators are busy
- Elevator state management (Idle, MovingUp, MovingDown, OutOfService)
- Floor range validation per elevator
- Freight elevator routing for heavy loads
- Interactive console simulation with status

## Technical Highlights

- Interface-based design — IElevator implemented by Elevator and FreightElevator
- Inheritance - FreightElevator extends Elevator with weight limit
- Encapsulation — elevators manage their own state and stops
- Exception handling with meaningful error messages
- Unit tests with NUnit covering all core logic
- Clean separation of concerns — Elevator, ElevatorManager

## Project Structure
```
ElevatorApp/
├── IElevator.cs # Elevator contract
├── Elevator.cs # Core elevator logic
├── FreightElevator.cs # Heavy load elevator
├── ElevatorState.cs # Elevator state enum
├── FloorRequest.cs # Passenger request model
├── ElevatorManager.cs # Dispatch logic and simulation
└── Program.cs # Console demo
ElevatorAppTests/
├── ElevatorTests.cs
├── FreightElevatorTests.cs
├── FloorRequestTests.cs
└── ElevatorManagerTests.cs
```

## Known Limitations

Elevator may incorrectly stop at a floor while passing in the wrong direction
when assigned stops in both directions simultaneously. This is a known tradeoff
of using HashSet for stop storage without directional context.

## Tech Stack

- C# / .NET
- NUnit for unit testing
- JetBrains Rider

## How to Run

1. Clone the repository
2. Open `SmartElevator.sln` in Rider or Visual Studio
3. Run `ElevatorApp` project
4. Follow the interactive menu

## How to Run Tests
Open solution in Rider and run `ElevatorAppTests` via built-in test runner.
