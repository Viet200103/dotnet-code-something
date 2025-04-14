namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Mac;

public class MacAdvancedToolbar : IToolbar
{
    public void AddButton(string label, Action click)
    {
        Console.WriteLine($"macOS Advanced Toolbar: Adding stylized button '{label}'");
    }

    public void Render()
    {
        Console.WriteLine("macOS Advanced Toolbar: Rendering with animations...");
    }
}