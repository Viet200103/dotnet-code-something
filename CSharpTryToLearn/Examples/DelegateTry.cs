namespace CSharpTryToLearn.Examples;

public class DelegateTry
{

    public delegate int AddNum(int a, int b);
    
    public static int Sum(int num1, int num2)
    {
        return num1 + num2;
    }

    class Rectangle
    {
        public delegate void RectDelegate(int width, int height);

        public void Area(int width, int height)
        {
            Console.WriteLine("Area is: {0}", (width * height));
        }

        public void Perimeter(int width, int height)
        {
            Console.WriteLine("Perimeter is: {0} ", 2 * (width + height));
        }
    }
    
    public static void Run()
    {
        Console.WriteLine("// DelegateTry example");
        AddNum addNum = Sum;
        Console.WriteLine(addNum(10, 20));
        Console.WriteLine();
        //////////////////////////
        Console.WriteLine("// Multicast delegate");
        Rectangle rectangle = new Rectangle();
        Rectangle.RectDelegate rectDelegate = rectangle.Area;
        rectDelegate += rectangle.Perimeter;
        rectDelegate.Invoke(10, 20);
        Console.WriteLine();
        /////////////////////////
        Console.WriteLine("// Func delegate for return result value of function");
        Console.WriteLine("// Sum function example");
        Func<int, int, int> sum = DelegateTry.Sum;
        Console.WriteLine(sum(10, 20));
        Console.WriteLine();
        /////////////////////////
        Console.WriteLine("// Func delegate for not return result value of function");
        Action<String> val = delegate(string str)
        {
            Console.WriteLine($"This is example for action delegate, {str}");
        };
        val.Invoke("Hello world");
        Console.WriteLine();
        /////////////////////////
        Console.WriteLine("// Predicate delegate, contain some set of criteria and determine whether the passed parameter fulfill the given criteria or not");
        Predicate<string> pre = delegate(string str)
        {
            Console.WriteLine($"Predicate: {str}");
            if (str.Length < 7)
            {
                return false;
            }
            return true;
        };
        Console.WriteLine(pre("Hello world") ? "String is valid" : "String is not valid");
    }
}