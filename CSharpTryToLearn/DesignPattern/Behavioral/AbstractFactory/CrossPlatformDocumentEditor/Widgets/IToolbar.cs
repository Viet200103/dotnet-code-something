namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets;

public interface IToolbar
{
    void AddButton(string label, Action click);
    void Render();
}