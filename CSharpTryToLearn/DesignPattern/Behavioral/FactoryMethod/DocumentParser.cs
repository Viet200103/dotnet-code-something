using CSharpTryToLearn.DesignPattern.Behavioral.AbstractFactory.CrossPlatformDocumentEditor;

namespace CSharpTryToLearn.DesignPattern.Behavioral.FactoryMethod;

public class DocumentParser
{
    
    public interface IDocumentParser
    {
        void Parse(string content);
    }
    
    public class JsonParser : IDocumentParser
    {
        public void Parse(string content)
        {
            Console.WriteLine("Parsing JSON: " + content);
        }
    }
    
    public class XmlParser : IDocumentParser
    {
        public void Parse(string content)
        {
            Console.WriteLine("Parsing XML: " + content);
        }
    }
    
    public abstract class DocumentParserFactory
    {
        public abstract IDocumentParser CreateParser();
    }
    
    public class JsonParserFactory : DocumentParserFactory
    {
        public override IDocumentParser CreateParser()
        {
            return new JsonParser();
        }
    }
    
    public class XmlParserFactory : DocumentParserFactory
    {
        public override IDocumentParser CreateParser()
        {
            return new XmlParser();
        }
    }
    
    public class DocumentParserProgram
    {
        public static void Run()
        {
            DocumentParserFactory documentParserFactory;
            string fileType = "json"; 
            string content = "{ \"name\": \"John\" }";

            if (fileType == "json")
            {
                documentParserFactory = new JsonParserFactory();
            }
            else if (fileType == "xml")
            {
                documentParserFactory = new XmlParserFactory();
                content = "<name>John</name>";
            }
            else
            {
                throw new NotSupportedException("Unsupported file type.");
            }
            
            IDocumentParser parser = documentParserFactory.CreateParser();
            parser.Parse(content);
        }
    }
}