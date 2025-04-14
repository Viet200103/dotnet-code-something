using CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets;
using CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Window;

namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor;

public class WindowsBasicFactory : IDocumentEditorFactory
{
    public IToolbar CreateToolbar()
    {
        return new WindowsBasicToolbar();
    }

    public ITextEditor CreateTextEditor()
    {
        return new WindowsBasicTextEditor();
    }

    public IStatusBar CreateStatusBar()
    {
        return new WindowsBasicStatusBar();
    }
}