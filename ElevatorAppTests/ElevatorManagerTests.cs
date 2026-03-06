using ElevatorApp;

namespace ElevatorTests;

public class ElevatorManagerTests
{
   private ElevatorManager _manager;
   private Elevator _elevator1;
   private Elevator _elevator2;
   private FreightElevator _freightElevator;

   [SetUp]
   public void Setup()
   {
      _elevator1 = new Elevator(1, 1, 10);
      _elevator2 = new Elevator(1, 1, 10);
      _freightElevator = new FreightElevator(1, 1, 10);

      _manager = new ElevatorManager(new List<Elevator>
      {
         _elevator1,
         _elevator2,
         _freightElevator
      });
   }

   [Test]
   public void ElevatorManager_WithNullElevator_ShouldThrow()
   {
      Assert.Throws<ArgumentNullException>(() => new ElevatorManager(null));
   }
   
   [Test]
   public void ElevatorManager_WithEmptyElevatorList_ShouldThrow()
   {
      Assert.Throws<ArgumentException>(() => new ElevatorManager(new List<Elevator>()));
   }

   [Test]
   public void ElevatorManager_WithValidElevatorList_ShouldStoreElevators()
   {
      Assert.That(_manager.Elevators, Is.EquivalentTo(new []{_elevator1, _elevator2, _freightElevator}));
   }
   
   [Test]
   public void SetOutOfService_WithInvalidIndex_ShouldThrow()
   {
      Assert.Throws<ArgumentOutOfRangeException>(() => _manager.SetOutOfService(10));
   }

   [Test]
   public void SetOutOfService_WithValidIndex_ShouldSetStateToOutOfService()
   {
      _manager.SetOutOfService(0);
      Assert.That(_manager.Elevators[0].State, Is.EqualTo(ElevatorState.OutOfService));
   }
   
   [Test]
   public void SetInService_WithInvalidIndex_ShouldThrow()
   {
      Assert.Throws<ArgumentOutOfRangeException>(() => _manager.SetInService(10));
   }

   [Test]
   public void SetInService_WithValidIndex_ShouldSetStateToIdle()
   {
      _manager.SetInService(0);
      Assert.That(_manager.Elevators[0].State, Is.EqualTo(ElevatorState.Idle));
   }
   
   [Test]
   public void RequestElevator_WithIdleElevator_ShouldAssignStops()
   {
      _manager.RequestElevator(new FloorRequest(1, 5));
      Assert.That(_elevator1.Stops.Contains(5), Is.True);
   }

   [Test]
   public void RequestElevator_WithNoIdleElevator_ShouldAddToPending()
   {
      _manager.RequestElevator(new FloorRequest(1, 10));
      _manager.RequestElevator(new FloorRequest(1, 10));
      _manager.RequestElevator(new FloorRequest(1, 10));
      _manager.RequestElevator(new FloorRequest(2, 8));
      Assert.That(_manager.PendingRequests.Count, Is.EqualTo(1));
   }

   [Test]
   public void RequestElevator_RequiresFreight_ShouldAssignToFreightElevator()
   {
      _manager.RequestElevator(new FloorRequest(1, 5, true));
      Assert.That(_freightElevator.Stops.Contains(5), Is.True);
   }

   [Test]
   public void Tick_ShouldMoveElevators()
   {
      _manager.RequestElevator(new FloorRequest(1, 5));
      _manager.Tick();
      Assert.That(_elevator1.CurrentFloor, Is.EqualTo(2));
   }

   [Test]
   public void Tick_ShouldProcessPendingRequests()
   {
      _manager.RequestElevator(new FloorRequest(1, 10));
      _manager.RequestElevator(new FloorRequest(1, 10));
      _manager.RequestElevator(new FloorRequest(1, 10));
      _manager.RequestElevator(new FloorRequest(2, 8));

      for (int i = 0; i < 20; i++)
         _manager.Tick();

      Assert.That(_manager.PendingRequests.Count, Is.EqualTo(0));
   }
}