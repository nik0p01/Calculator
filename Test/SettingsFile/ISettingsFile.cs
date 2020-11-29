using System.Collections.Generic;

namespace Test.SettingsFile
{
    public interface ISettingsFile
    {
        Dictionary<string, List<string>> Settings { get; set; }
        string Path { get; }
        void GetSettingsFromFile();
        void CreateDefaultFileSettings();
        void SaveSettingsToFile();
    }
}
