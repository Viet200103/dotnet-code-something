namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets;

public interface ITextEditor
{
    void SetText(string text);
    string GetText();
    void Display();
}