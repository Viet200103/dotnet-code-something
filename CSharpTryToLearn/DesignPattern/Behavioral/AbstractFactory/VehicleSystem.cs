namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory;

public class VehicleSystem
{
    
    public interface IEngine
    {
        void Start();
        string GetDetails();
    }

    public interface ITire
    {
        void Rotate();
        string GetDetails();
    }

    public interface IVehicleFactory
    {
        IEngine CreateEngine();
        ITire CreateTire();
    }

    private class CarEngine : IEngine
    {
        public void Start()
        {
            Console.WriteLine("Car engine starts with a smooth hum");
        }

        public string GetDetails()
        {
            return "4-cylinder car engine";
        }
    }

    private class CarTire : ITire
    {
        public void Rotate()
        {
            Console.WriteLine("Car tire rotates steadily");
        }
        public string GetDetails() => "All-season car tire";
    }

    private class MotorcycleEngine : IEngine
    {
        public void Start()
        {
            Console.WriteLine("Motorcycle engine revs loudly");
        }

        public string GetDetails()
        {
            return "Single-cylinder motorcycle engine";
        }
    }

    private class MotorcycleTire : ITire
    {
        public void Rotate() => Console.WriteLine("Motorcycle tire spins aggressively");
        public string GetDetails() => "Sport motorcycle tire";
    }

    private class CarFactory : IVehicleFactory
    {
        public IEngine CreateEngine()
        {
            return new CarEngine();
        }

        public ITire CreateTire()
        {
            return new CarTire();
        }
    }
    
    public class MotorcycleFactory : IVehicleFactory
    {
        public IEngine CreateEngine()
        {
            return new MotorcycleEngine();
        }

        public ITire CreateTire()
        {
            return new MotorcycleTire();
        }
    }

    private class VehicleAssembler
    {
        private readonly IEngine _engine;
        private readonly ITire _tire;

        public VehicleAssembler(IVehicleFactory factory)
        {
            _engine = factory.CreateEngine();
            _tire = factory.CreateTire();
        }

        public void AssembleVehicles()
        {
            Console.WriteLine($"Assembling vehicle with:");
            Console.WriteLine($" - Engine: {_engine.GetDetails()}");
            Console.WriteLine($" - Tire: {_tire.GetDetails()}");
            _engine.Start();
            _tire.Rotate();
        }
    }

    public class VehicleProgram
    {

        public static void Run()
        {
            IVehicleFactory carFactory = new CarFactory();
            VehicleAssembler carAssembler = new VehicleAssembler(carFactory);
            Console.WriteLine("Car Assembly:");
            carAssembler.AssembleVehicles();
            
            Console.WriteLine("----------------------------------------------");
            IVehicleFactory motorcycleFactory = new MotorcycleFactory();
            VehicleAssembler motorcycleAssembler = new VehicleAssembler(motorcycleFactory);
            Console.WriteLine("Motorcycle Assembly:");
            motorcycleAssembler.AssembleVehicles();
        }
    }
}