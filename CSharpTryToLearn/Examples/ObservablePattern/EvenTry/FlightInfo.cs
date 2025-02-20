namespace CSharpTryToLearn.Examples.ObservablePattern.EvenTry;

public class FlightInfo
{
    private readonly string _flightNumber;
    private readonly string _from;

    public FlightInfo(string flightNumber, string from)
    {
        _flightNumber = flightNumber;
        _from = from;
    }
    
    public string FlightNumber => _flightNumber;
    public string From => _from;
}