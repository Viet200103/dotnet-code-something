namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Window;

public class WindowsBasicStatusBar : IStatusBar
{
    public void SetStatus(string message)
    {
        Console.WriteLine($"Windows Basic Status Bar: Status - {message}");
    }

    public void Display()
    {
        Console.WriteLine("Windows Basic Status Bar: Displaying...");
    }
}