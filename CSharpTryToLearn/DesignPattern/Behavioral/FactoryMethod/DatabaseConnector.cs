namespace CSharpTryToLearn.DesignPattern.Behavioral.FactoryMethod;

public class DatabaseConnector
{
    
    public interface IDatabaseConnector
    {
        void Connect();
    }
    
    public class SqlServerConnector : IDatabaseConnector
    {
        public void Connect()
        {
            Console.WriteLine("Connecting to SQL Server...");
            // Simulate SQL Server connection logic
        }
    }

    public class MySqlConnector : IDatabaseConnector
    {
        public void Connect()
        {
            Console.WriteLine("Connecting to MySQL...");
            // Simulate MySQL connection logic
        }
    }
    
    public abstract class DatabaseConnectionCreator
    {
        public abstract IDatabaseConnector CreateConnector();
    }
    
    public class SqlServerConnectionCreator : DatabaseConnectionCreator
    {
        public override IDatabaseConnector CreateConnector()
        {
            return new SqlServerConnector();
        }
    }
    
    public class MySqlConnectorCreator : DatabaseConnectionCreator
    {
        public override IDatabaseConnector CreateConnector()
        {
            return new MySqlConnector();
        }
    }
    
    public class DatabaseConnectorProgram
    {
        
        public static void Run()
        {
            DatabaseConnectionCreator connectionCreator;
            string dbType = "mysql";

            switch (dbType)
            {
                case "sqlserver":
                    connectionCreator = new SqlServerConnectionCreator();
                    break;
                
                case "mysql":
                    connectionCreator = new MySqlConnectorCreator();
                    break;
                default: throw new ArgumentException("Unknown db type");
            }
            
            IDatabaseConnector connector = connectionCreator.CreateConnector();
            connector.Connect();
        }
    }
}