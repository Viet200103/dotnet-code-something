namespace CSharpTryToLearn.Asynchronous;

public class ThreadingDemo
{
    public class NumberHelper
    {
        int _number;

        public NumberHelper(int number)
        {
            _number = number;
        }

        public void DisplayNumber()
        {
            for (int i = 1; i <= _number; i++)
            {
                Console.WriteLine("value : " + i);
            }
        }
    }

    public delegate void ResultCallback(int result);
    
    public class ResultNumberHelper
    {
        
        private int _number;
        private ResultCallback _resultCallback;


        public ResultNumberHelper(int number, ResultCallback resultCallback)
        {
            _number = number;
            _resultCallback = resultCallback;
        }

        public void CalculateSum()
        {
            int result = 0;
            for (int i = 0; i < _number; i++)
            {
                result += i;
            }

            if (_resultCallback != null)
            {
                _resultCallback.Invoke(result);
            }
        }
        
    }

    public static void runResultWithThread()
    {
        ResultCallback resultCallback = OnResult;
        int number = 10;
        
        ResultNumberHelper obj = new ResultNumberHelper(number, resultCallback);
        
        Thread t = new Thread(obj.CalculateSum);
        t.Start();
    }

    public static void OnResult(int result)
    {
        Console.WriteLine("The Result is " + result);
    }
    
    public static void Run()
    {
        int max = 10;
        NumberHelper numberHelper = new NumberHelper(max);
        
        Thread t1 = new Thread(numberHelper.DisplayNumber);
        
        t1.Start();
        Console.Read();
    }
}