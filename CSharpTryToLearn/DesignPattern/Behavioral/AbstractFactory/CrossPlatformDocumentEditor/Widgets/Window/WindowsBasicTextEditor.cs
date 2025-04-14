namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Window;

public class WindowsBasicTextEditor : ITextEditor
{

    private string _text = "";
    
    public void SetText(string text)
    {
        _text = text;
    }

    public string GetText()
    {
        return _text;
    }

    public void Display()
    {
        Console.WriteLine("Windows Basic Text Editor: Displaying text (basic)...");
        Console.WriteLine(_text);
    }
}