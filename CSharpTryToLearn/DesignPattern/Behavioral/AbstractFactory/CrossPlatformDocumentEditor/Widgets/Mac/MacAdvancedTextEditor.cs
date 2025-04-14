namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Mac;

public class MacAdvancedTextEditor : ITextEditor
{
    private string _text = String.Empty;
    
    public void SetText(string text) => _text = text;

    public string GetText() => _text;

    public void Display()
    {
        Console.WriteLine("macOS Advanced Text Editor: Displaying text with syntax highlighting...");
        Console.WriteLine(_text);
    }
}