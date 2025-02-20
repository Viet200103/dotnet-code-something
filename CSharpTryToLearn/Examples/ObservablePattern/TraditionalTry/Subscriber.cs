using CSharpTryToLearn.Examples.ObservablePattern.EvenTry;

namespace CSharpTryToLearn.Examples.ObservablePattern.TraditionalTry;

public class Subscriber : IObserver<FlightInfo>
{
    public string Name { get; set; }
    private IDisposable cancellation;

    public Subscriber()
    {
        
    }
    
    public Subscriber(string name)
    {
        Name = name;
    }

    public virtual void Subscribe(IObservable<FlightInfo> observable)
    {
        cancellation = observable.Subscribe(this);
    }
    
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(FlightInfo value)
    {
        Console.WriteLine("{0} received data from publisher", Name);
        Console.WriteLine("Flight number: {0}", value.FlightNumber);
        Console.WriteLine("From: {0}", value.From);
    }

    public virtual void Unsubscribe()
    {
        cancellation.Dispose();
    }
}