namespace CSharpTryToLearn.DesignPattern.Behavioral.Prototype;

public class ConstructorPrototype
{

    class ConcretePrototypeA
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ConcretePrototypeA() {}

        public ConcretePrototypeA(ConcretePrototypeA prototype)
        {
            X = prototype.X;
            Y = prototype.Y;
        }
    }

    public static void Run()
    {
        ConcretePrototypeA prototypeA = new ConcretePrototypeA();
        prototypeA.X = 10;
        prototypeA.Y = 20;

        // Use the copy constructor to create a new instance of ConcretePrototypeA
        ConcretePrototypeA clone = new ConcretePrototypeA(prototypeA);
        
    }
}