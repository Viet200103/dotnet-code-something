
namespace CSharpTryToLearn.Examples;

public class EventDemo
{

    public class MyEventArgs : EventArgs
    {
        
        public MyEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
    
    public class ClassA
    {
        public event EventHandler<MyEventArgs> event_news;
        
        public void Send()
        {
            event_news?.Invoke(this, new MyEventArgs("Hello World"));
        }
    }
    
    public class ClassB
    {
        public void Sub(ClassA a)
        {
            a.event_news += ReceiverFromPublisher;
        }

        private void ReceiverFromPublisher(object? sender, MyEventArgs e)
        {
            Console.WriteLine ("ClassB: " + e.Message);
        }
    }
    
    public class ClassC
    {
        public void Sub(ClassA a)
        {
            a.event_news += ReceiverFromPublisher;
        }

        private void ReceiverFromPublisher(object? sender, MyEventArgs e)
        {
            Console.WriteLine ("ClassC: " + e.Message);
        }
    }
    
    public static void Run()
    {
        ClassA p  = new ClassA();
        ClassB sa = new ClassB();
        ClassC sb = new ClassC();

        sa.Sub (p); // sa đăng ký nhận sự kiện từ p
        sb.Sub (p); // sb đăng ký nhận sự kiện từ p

        p.Send ();
    }
}