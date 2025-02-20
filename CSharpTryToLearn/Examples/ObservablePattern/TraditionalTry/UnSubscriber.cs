using CSharpTryToLearn.Examples.ObservablePattern.EvenTry;

namespace CSharpTryToLearn.Examples.ObservablePattern.TraditionalTry;

public class UnSubscriber : IDisposable
{
    
    private readonly List<IObserver<FlightInfo>> _observers;
    private readonly IObserver<FlightInfo> _observer;

    internal UnSubscriber(List<IObserver<FlightInfo>> observers, IObserver<FlightInfo> observer)
    {
        _observers = observers;
        _observer = observer;
    }
    
    public void Dispose()
    {
        if (_observers.Contains(_observer))
        {
            _observers.Remove(_observer);
        }
    }
}