namespace CSharpTryToLearn.Asynchronous;

public class ParallelProcessingDemo
{

    internal class Matrix
    {
        public void Rotate(float degrees)
        {
            Console.WriteLine($"Rotating {degrees} degrees");
        }

        public bool IsInvertible { get; set; }

        public void Invert()
        {
            Console.WriteLine("Matrix is inverted");
        }
    }
    
    static void RotateMatrices(IEnumerable<Matrix> matrices, float degrees)
    {
        Parallel.ForEach(matrices, matrix => matrix.Rotate(degrees));
    }

    static void InvertMatrices(IEnumerable<Matrix> matrices, float degrees)
    {
        Parallel.ForEach(matrices, (matrix, state) =>
        {
            if (!matrix.IsInvertible)
            {
                state.Stop();
            }
            else
            {
                matrix.Invert();
            }
        });
    }

    static int InvertMatricesSync(IEnumerable<Matrix> matrices)
    {
        object mutex = new object();
        int nonInvertibleCount = 0;
        
        Parallel.ForEach(matrices, matrix =>
        {
            if (matrix.IsInvertible)
            {
                matrix.Invert();
            }
            else
            {
                lock (mutex)
                {
                    ++nonInvertibleCount;
                }
            }
        });
        return nonInvertibleCount;
    }


    static int ParallelSum(IEnumerable<int> values)
    {
        object mutex = new object();
        int result = 0;

        Parallel.ForEach(
            source: values,
            localInit: () => 0,
            body: (item, state, localValue) => localValue + item,
            localFinally: localValue =>
            {
                lock (mutex)
                {
                    result += localValue;
                }
            }
        );

        return result;
    }

    public static void Run()
    {
        var nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var result = ParallelSum(nums);

        Console.WriteLine(result);
    }
}

