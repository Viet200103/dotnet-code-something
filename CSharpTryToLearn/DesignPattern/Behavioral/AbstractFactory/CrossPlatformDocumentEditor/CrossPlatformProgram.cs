namespace CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor;

public class CrossPlatformProgram
{

    public static void Run()
    {
        IDocumentEditorFactory factory;
        string platform = "macOS"; 
        string rendering = "advanced";

        switch (platform.ToLower())
        {
            case "windows":
                if (rendering.ToLower() == "basic")
                {
                    factory = new WindowsBasicFactory();
                }
                else
                {
                    Console.WriteLine("Windows with advanced rendering not yet implemented.");
                    return;
                }
                break;
            
            case "macos":
                if (rendering.ToLower() == "advanced")
                {
                    factory = new MacAdvancedFactory();
                }
                else
                {
                    Console.WriteLine("macOS with basic rendering not yet implemented.");
                    return;
                }
                break;
            
            default: throw new Exception("Unknown platform");
        }

        var app = new DocumentEditorApp(factory);
        app.Run();
    }
}