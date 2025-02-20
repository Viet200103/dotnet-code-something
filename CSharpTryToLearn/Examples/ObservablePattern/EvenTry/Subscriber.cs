namespace CSharpTryToLearn.Examples.ObservablePattern.EvenTry;

internal class Subscriber
{
    public string Name { get; set; } = String.Empty;

    public Subscriber()
    {
        
    }

    public Subscriber(string name)
    {
        Name = name;
    }

    public void Subscribe(Publisher publisher)
    {
        publisher.OnFlightArrived += ReceiveDataFromPublisher;
    }

    public void Unsubscribe(Publisher publisher)
    {
        publisher.OnFlightArrived -= ReceiveDataFromPublisher;
    }
    
    private void ReceiveDataFromPublisher(object s, FlightInfo e)
    {
        Console.WriteLine("{0} received data from publisher", Name);
        Console.WriteLine("Flight number: {0}", e.FlightNumber);
        Console.WriteLine("From: {0}", e.From);
    }
}
