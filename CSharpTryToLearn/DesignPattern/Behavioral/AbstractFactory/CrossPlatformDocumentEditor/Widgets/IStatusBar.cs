namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets;

public interface IStatusBar
{
    void SetStatus(string message);
    void Display();
}