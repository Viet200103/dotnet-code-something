namespace CSharpTryToLearn.DesignPattern.Behavioral.Prototype;

// Deep copy for prototype
public class ReportPrototype
{
    private interface IPrototype<T>
    {
        T Clone();
    }
    
    public class ReportSettings
    {
        public string Orientation { get; set; } 
        public int FontSize { get; set; }
    }
    
    public class Report : IPrototype<Report>
    {
        
        public string Title { get; set; }
        public string Author { get; set; }
        public ReportSettings Settings { get; set; }
        
        public Report Clone()
        {
            return new Report
            {
                Title = this.Title,
                Author = this.Author,
                Settings = new ReportSettings
                {
                    Orientation = this.Settings.Orientation,
                    FontSize = this.Settings.FontSize
                }
            };
        }
        
        public void Display()
        {
            Console.WriteLine($"Report: {Title} by {Author} - Orientation: {Settings.Orientation}, Font Size: {Settings.FontSize}");
        }
    }

    public static void Run()
    {
        // Original report
        Report originalReport = new Report
        {
            Title = "Monthly Sales",
            Author = "Alice",
            Settings = new ReportSettings
            {
                Orientation = "Landscape",
                FontSize = 12
            }
        };
            
        Report cloneReport = originalReport.Clone();
            
        cloneReport.Title = "Monthly Sales";
        cloneReport.Settings.FontSize = 14;
            
        originalReport.Display();
        cloneReport.Display();
    }
}