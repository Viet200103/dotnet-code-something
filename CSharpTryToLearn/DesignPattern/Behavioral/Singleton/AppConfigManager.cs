
using System.Text.Json;
using System.Xml;

namespace CSharpTryToLearn.DesignPattern.Behavioral.Singleton;

public class AppConfigManager
{
    private static readonly Lazy<AppConfigManager> _instance = new(() => new AppConfigManager());

    private readonly Dictionary<string, string> _config = new();
    private readonly string _configFilePath = "config.json";
    
    private AppConfigManager()
    {
        if (File.Exists(_configFilePath))
        {
            string json = File.ReadAllText(_configFilePath);
            _config = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }
    }
    
    public static AppConfigManager Instance => _instance.Value;
    
    public void SetSetting(string key, string value)
    {
        _config[key] = value;
        File.WriteAllText(_configFilePath, JsonSerializer.Serialize(_config));
    }
}