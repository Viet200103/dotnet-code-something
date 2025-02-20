using CSharpTryToLearn.Examples.ObservablePattern.EvenTry;

namespace CSharpTryToLearn.Examples.ObservablePattern.TraditionalTry;

public class Publisher : IObservable<FlightInfo>
{
    private readonly List<IObserver<FlightInfo>> _observers;

    public Publisher()
    {
        _observers = new List<IObserver<FlightInfo>>();
    }
        
    public IDisposable Subscribe(IObserver<FlightInfo> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
        return new UnSubscriber(_observers, observer);
    }

    public void Send(string flightNumber, string from)
    {
        var flightInfo = new FlightInfo(flightNumber, from);
        foreach (var item in _observers)
        {
            item.OnNext(flightInfo);
        }
    }
}