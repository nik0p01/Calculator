using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace Test.SettingsFile
{
    public class SettingsFileJson : ISettingsFile
    {
        //TODO: Singleton would be better
        public SettingsFileJson(string path)
        {
            _settings = new Dictionary<string, List<string>>();
            Path = path;
        }

        private Dictionary<string, List<string>> _settings;
        public Dictionary<string, List<string>> Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public string Path { get; private set; }

        public void CreateDefaultFileSettings()
        {
            _settings.Add("func", new List<string>() { "add", "sub", "mult" });
            _settings.Add("funcCurrent", new List<string>() { "add" });
            _settings.Add("DefaultValue", new List<string>() { (3.3).ToString("G", CultureInfo.InvariantCulture), (2.1).ToString("G", CultureInfo.InvariantCulture) });
            SaveSettingsToFile();
        }

        public void GetSettingsFromFile()
        {
            string json = File.ReadAllText(Path);
            _settings = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
        }

        public void SaveSettingsToFile()
        {
            string json = JsonSerializer.Serialize<Dictionary<string, List<string>>>(_settings);
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            File.AppendAllText(Path, json);
        }
    }
}
