using CSharpTryToLearn.Examples.ObservablePattern.EvenTry;

namespace CSharpTryToLearn.Examples.ObservablePattern;

public class ObservablePatternEntry
{
    public static void EventRun()
    {
        var publisher = new Publisher();
        var subA = new Subscriber("Sub A");
        var subB = new Subscriber("Sub B");

        subA.Subscribe(publisher);
        subB.Subscribe(publisher);
        publisher.Send("1", "Viet Nam");
    }

    public static void TraditionalRun()
    {
        var publisher = new Examples.ObservablePattern.TraditionalTry.Publisher();
        var subA = new Examples.ObservablePattern.TraditionalTry.Subscriber("Sub A");
        var subB = new Examples.ObservablePattern.TraditionalTry.Subscriber("Sub B");

        subA.Subscribe(publisher);
        subB.Subscribe(publisher);
        publisher.Send("1", "Viet Nam");
    }
}