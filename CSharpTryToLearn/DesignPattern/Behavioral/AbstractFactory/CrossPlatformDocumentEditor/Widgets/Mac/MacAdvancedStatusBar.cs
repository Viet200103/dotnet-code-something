namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Mac;

public class MacAdvancedStatusBar : IStatusBar
{
    public void SetStatus(string message)
    {
        Console.WriteLine($"macOS Advanced Status Bar: Status - {message} (with icon)");
    }

    public void Display()
    {
        Console.WriteLine("macOS Advanced Status Bar: Displaying with progress bar...");
    }
}