namespace CSharpTryToLearn.Examples.ObservablePattern.EvenTry;

internal class Publisher
{
    public event EventHandler<FlightInfo> OnFlightArrived;

    public void Send(string flightNumber, string from)
    {
        var flightInfo = new FlightInfo(flightNumber, from);
        OnFlightArrived.Invoke(flightInfo, flightInfo);
    }
}