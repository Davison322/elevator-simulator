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
   

}