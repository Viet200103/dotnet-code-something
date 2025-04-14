namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Window;

public class WindowsBasicToolbar : IToolbar
{
    public void AddButton(string label, Action click)
    {
        Console.WriteLine($"Windows Basic Toolbar: Adding button '{label}'");
    }

    public void Render()
    {
        Console.WriteLine("Windows Basic Toolbar: Rendering...");
    }
}