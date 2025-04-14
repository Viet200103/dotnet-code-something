using CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets;

namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor;

public interface IDocumentEditorFactory
{
    IToolbar CreateToolbar();
    ITextEditor CreateTextEditor();
    IStatusBar CreateStatusBar();
}