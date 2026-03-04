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
}