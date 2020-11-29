using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Test.SettingsFile;

namespace Test
{
    class Program
    {
        private static ISettingsFile _settings = new SettingsFileJson("appsettings.json");
        public static void Main(string[] args)
        {
            if (!File.Exists(_settings.Path))
            {
                _settings.CreateDefaultFileSettings();
            }
            _settings.GetSettingsFromFile();

            GetArgs(args, out double? first, out double? second, out string functionName);
            SetValueIfNull(ref first, ref second, ref functionName);

            if (!CheckFunctionName(functionName))
            {
                Console.Write("The configuration file does not contain this function");
                return;
            }

            SetCurrentFunction(functionName);
            double result;
            Calculator calculate = new Calculator(functionName);
            try
            {
                result = calculate.TestFunction(first.Value, second.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is a problem: {ex.Message}. The app is closing");
                Console.ReadKey();
                return;
            }

            Console.WriteLine(result.ToString("G", CultureInfo.InvariantCulture));
            Console.ReadKey();

        }

        private static void SetCurrentFunction(string func)
        {
     
            _settings.Settings["funcCurrent"][0] = func;
            _settings.SaveSettingsToFile();
        }

        private static bool CheckFunctionName(string func)
        {
            return _settings.Settings["func"].Count(f => f == func) > 0;
        }

        private static void SetValueIfNull(ref double? first, ref double? second, ref string func)
        {
            if (first == null)
            {
                first = TryParseOrNull(_settings.Settings["DefaultValue"][0]);
            }
            if (second == null)
            {
                second = TryParseOrNull(_settings.Settings["DefaultValue"][1]);
            }
            if (func == null)
            {
                func = _settings.Settings["funcCurrent"][0];
            }
        }
        private static void GetArgs(string[] args, out double? first, out double? second, out string func)
        {
            first = null;
            second = null;
            func = null;
            if (args.Length >= 2)
            {
                first = TryParseOrNull(args[0]);
                second = TryParseOrNull(args[1]);
            }
            if (args.Length >= 3)
            {
                func = args[2];
            }
        }
        private static Double? TryParseOrNull(string args)
        {
            double? value = null;
            if (Double.TryParse(args, System.Globalization.NumberStyles.Float,
    CultureInfo.InvariantCulture, out double firstValue))
                value = firstValue;
            return value;
        }
    }
}