using System.Runtime.CompilerServices;
using Config.Net;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace jLib
{
    public static class ConsoleUi
    {
        public enum Result
        {
            Success,
            Fail,
            Yes,
            No,
            Cancel,
        }

        public enum MessageType
        {
            None,
            Info,
            Warning,
            Error,
        }

        public static Result Log(string message, MessageType messageType, string callingPackage, Exception? e = null)
        {
            // TODO: add ability to override to allow use on different UI implementations (Win Forms, Avalonia UI, etc.)
            var messageTypeString = messageType switch
            {
                MessageType.None => "",
                MessageType.Info => "[INFO] ",
                MessageType.Warning => "[WARNING] ",
                MessageType.Error => "[ERROR] ",
                _ => ""
            };
            if (messageTypeString == "[ERROR]")
            {
                throw new ApplicationException("[ERROR] [" + callingPackage + "] " + message, e);
            }

            Console.WriteLine(messageTypeString + "[" + callingPackage + "] " + message);
            return Result.Success;
        }

        public static Result YesNoPrompt(string message, string callingPackage, MessageType type = MessageType.None)
        {
            // TODO: add ability to override to allow use on different UI implementations (Win Forms, Avalonia UI, etc.)
            var messageTypeString = type switch
            {
                MessageType.None => "",
                MessageType.Info => "[INFO] ",
                MessageType.Warning => "[WARNING] ",
                MessageType.Error => "[ERROR] ",
                _ => ""
            };
            Console.WriteLine(messageTypeString + "[" + callingPackage + "] " + message + " Y/N");
            while (true)
            {
                while (Console.KeyAvailable == false)
                    Thread.Sleep(250); // Loop until input is entered.

                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Y:
                        return Result.Yes;
                    case ConsoleKey.N:
                        return Result.No;
                    case ConsoleKey.None:
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Tab:
                    case ConsoleKey.Clear:
                    case ConsoleKey.Enter:
                    case ConsoleKey.Pause:
                    case ConsoleKey.Escape:
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.PageUp:
                    case ConsoleKey.PageDown:
                    case ConsoleKey.End:
                    case ConsoleKey.Home:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.Select:
                    case ConsoleKey.Print:
                    case ConsoleKey.Execute:
                    case ConsoleKey.PrintScreen:
                    case ConsoleKey.Insert:
                    case ConsoleKey.Delete:
                    case ConsoleKey.Help:
                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                    case ConsoleKey.A:
                    case ConsoleKey.B:
                    case ConsoleKey.C:
                    case ConsoleKey.D:
                    case ConsoleKey.E:
                    case ConsoleKey.F:
                    case ConsoleKey.G:
                    case ConsoleKey.H:
                    case ConsoleKey.I:
                    case ConsoleKey.J:
                    case ConsoleKey.K:
                    case ConsoleKey.L:
                    case ConsoleKey.M:
                    case ConsoleKey.O:
                    case ConsoleKey.P:
                    case ConsoleKey.Q:
                    case ConsoleKey.R:
                    case ConsoleKey.S:
                    case ConsoleKey.T:
                    case ConsoleKey.U:
                    case ConsoleKey.V:
                    case ConsoleKey.W:
                    case ConsoleKey.X:
                    case ConsoleKey.Z:
                    case ConsoleKey.LeftWindows:
                    case ConsoleKey.RightWindows:
                    case ConsoleKey.Applications:
                    case ConsoleKey.Sleep:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.NumPad9:
                    case ConsoleKey.Multiply:
                    case ConsoleKey.Add:
                    case ConsoleKey.Separator:
                    case ConsoleKey.Subtract:
                    case ConsoleKey.Decimal:
                    case ConsoleKey.Divide:
                    case ConsoleKey.F1:
                    case ConsoleKey.F2:
                    case ConsoleKey.F3:
                    case ConsoleKey.F4:
                    case ConsoleKey.F5:
                    case ConsoleKey.F6:
                    case ConsoleKey.F7:
                    case ConsoleKey.F8:
                    case ConsoleKey.F9:
                    case ConsoleKey.F10:
                    case ConsoleKey.F11:
                    case ConsoleKey.F12:
                    case ConsoleKey.F13:
                    case ConsoleKey.F14:
                    case ConsoleKey.F15:
                    case ConsoleKey.F16:
                    case ConsoleKey.F17:
                    case ConsoleKey.F18:
                    case ConsoleKey.F19:
                    case ConsoleKey.F20:
                    case ConsoleKey.F21:
                    case ConsoleKey.F22:
                    case ConsoleKey.F23:
                    case ConsoleKey.F24:
                    case ConsoleKey.BrowserBack:
                    case ConsoleKey.BrowserForward:
                    case ConsoleKey.BrowserRefresh:
                    case ConsoleKey.BrowserStop:
                    case ConsoleKey.BrowserSearch:
                    case ConsoleKey.BrowserFavorites:
                    case ConsoleKey.BrowserHome:
                    case ConsoleKey.VolumeMute:
                    case ConsoleKey.VolumeDown:
                    case ConsoleKey.VolumeUp:
                    case ConsoleKey.MediaNext:
                    case ConsoleKey.MediaPrevious:
                    case ConsoleKey.MediaStop:
                    case ConsoleKey.MediaPlay:
                    case ConsoleKey.LaunchMail:
                    case ConsoleKey.LaunchMediaSelect:
                    case ConsoleKey.LaunchApp1:
                    case ConsoleKey.LaunchApp2:
                    case ConsoleKey.Oem1:
                    case ConsoleKey.OemPlus:
                    case ConsoleKey.OemComma:
                    case ConsoleKey.OemMinus:
                    case ConsoleKey.OemPeriod:
                    case ConsoleKey.Oem2:
                    case ConsoleKey.Oem3:
                    case ConsoleKey.Oem4:
                    case ConsoleKey.Oem5:
                    case ConsoleKey.Oem6:
                    case ConsoleKey.Oem7:
                    case ConsoleKey.Oem8:
                    case ConsoleKey.Oem102:
                    case ConsoleKey.Process:
                    case ConsoleKey.Packet:
                    case ConsoleKey.Attention:
                    case ConsoleKey.CrSel:
                    case ConsoleKey.ExSel:
                    case ConsoleKey.EraseEndOfFile:
                    case ConsoleKey.Play:
                    case ConsoleKey.Zoom:
                    case ConsoleKey.NoName:
                    case ConsoleKey.Pa1:
                    case ConsoleKey.OemClear:
                    default:
                        break;
                }
            }
        }

        [Obsolete("OkPrompt() is deprecated, please use Log() instead.", true)]
        public static Result OkPrompt(string message, string title, string callingPackage, MessageType type = MessageType.None)
        {
            return Log("(" + title + ") " + message, type, callingPackage);
        }
    }
    public static class Fs
    {
        public static void CopyFiles(string sourceFolder, string targetFolder)
        {
            Directory.CreateDirectory(targetFolder);
            foreach (var directory in Directory.GetDirectories(sourceFolder))
            {
                var newDirectory = Path.Combine(targetFolder, Path.GetFileName(directory));
                Directory.CreateDirectory(newDirectory);
                CopyFiles(directory, newDirectory);
            }

            foreach (var file in Directory.GetFiles(sourceFolder))
            {
                if (Config.GetDebugMode())
                {
                    ConsoleUi.Log(file, ConsoleUi.MessageType.None, "jLib");
                }

                File.Copy(file, Path.Combine(targetFolder, Path.GetFileName(file)), true);
            }
        }

        public static void DeleteDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                var yesNoPrompt = ConsoleUi.YesNoPrompt("Are you sure you want to delete directory " + dir + "?", "jLib", ConsoleUi.MessageType.Warning);
                if (yesNoPrompt != ConsoleUi.Result.Yes) return;
                ConsoleUi.Log("Removing directory: " + dir, ConsoleUi.MessageType.Info, "jLib");
                Directory.Delete(dir, true);
            }
            else
            {
                ConsoleUi.Log("Directory" + dir + " does not exist", ConsoleUi.MessageType.Warning, "jLib");
            }
        }

        public static string GetUserFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        public static string GetAppDataFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }

    public static class Config
    {
        private static string? ConfigFolderName { get; set; }

        private static void CheckConfigFolderName()
        {
            if (ConfigFolderName == null)
            {
                ConsoleUi.Log("ConfigFolderName has not been set, please use SetConfigFolderName() when the app is starting!!",
                    ConsoleUi.MessageType.Error, 
                    "jLib");
            }
        }

        public static void SetConfigFolderName(string configFolderName)
        {
            ConfigFolderName = configFolderName;
        }

        public static bool GetDebugMode()
        {
            CheckConfigFolderName();
            var configLocation = Path.Combine(Fs.GetAppDataFolder(), ConfigFolderName!, "Config", "jLib.json");
            var settings = new ConfigurationBuilder<IJLibConfig>()
                .UseJsonFile(configLocation)
                .Build();
            return settings.DebugMode;
        }

        /**
         * Part of the Partially implemented Console UI Override Code
         */
        public static bool GetConsoleUiMode()
        {
            CheckConfigFolderName();
            var configLocation = Path.Combine(Fs.GetAppDataFolder(), ConfigFolderName!, "Config", "jLib.json");
            var settings = new ConfigurationBuilder<IJLibConfig>()
                .UseJsonFile(configLocation)
                .Build();
            return settings.ConsoleUiMode;
        }

        public static void SetDebugMode(bool debugMode)
        {
            CheckConfigFolderName();
            var configLocation = Path.Combine(Fs.GetAppDataFolder(), ConfigFolderName!, "Config", "jLib.json");
            var settings = new ConfigurationBuilder<IJLibConfig>()
                .UseJsonFile(configLocation)
                .Build();
            settings.DebugMode = debugMode;
        }

        /**
         * Part of the Partially implemented Console UI Override Code
         */
        public static void SetConsoleUiMode(bool consoleUiMode)
        {
            CheckConfigFolderName();
            var configLocation = Path.Combine(Fs.GetAppDataFolder(), ConfigFolderName!, "Config", "jLib.json");
            var settings = new ConfigurationBuilder<IJLibConfig>()
                .UseJsonFile(configLocation)
                .Build();
            settings.ConsoleUiMode = consoleUiMode;
        }
    }

    internal interface IJLibConfig
    {
        bool DebugMode { get; set; }
        bool ConsoleUiMode { get; set; }
    }
}
