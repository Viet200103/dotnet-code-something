using CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets;
using CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets.Mac;

namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor;

public class MacAdvancedFactory : IDocumentEditorFactory
{
    public IToolbar CreateToolbar() => new MacAdvancedToolbar();
    public ITextEditor CreateTextEditor() => new MacAdvancedTextEditor();
    public IStatusBar CreateStatusBar() => new MacAdvancedStatusBar();
}