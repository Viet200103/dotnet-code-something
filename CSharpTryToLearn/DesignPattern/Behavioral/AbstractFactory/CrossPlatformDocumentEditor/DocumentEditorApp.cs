using CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor.Widgets;

namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor;

public class DocumentEditorApp
{
    private readonly IDocumentEditorFactory _factory;
    private readonly IToolbar _toolbar;
    private readonly ITextEditor _textEditor;
    private readonly IStatusBar _statusBar;

    public DocumentEditorApp(IDocumentEditorFactory factory)
    {
        _factory = factory;
        _toolbar = _factory.CreateToolbar();
        _textEditor = _factory.CreateTextEditor();
        _statusBar = _factory.CreateStatusBar();
    }
    
    public void Run()
    {
        Console.WriteLine("Starting Document Editor...");
        _toolbar.AddButton("Open", () => Console.WriteLine("Opening file..."));
        _toolbar.AddButton("Save", () => Console.WriteLine("Saving file..."));
        _toolbar.Render();

        _textEditor.SetText("This is some important text.");
        _textEditor.Display();

        _statusBar.SetStatus("Ready");
        _statusBar.Display();

        Console.WriteLine("Document Editor finished.");
    }
}