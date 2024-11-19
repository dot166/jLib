using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace jLib
{
    public static class ConsoleUi
    {
        public enum Result
        {
            None,
            Ok,
            Yes,
            No,
            Abort,
            Cancel,
            Success,
            Halt,
        }

        public enum MessageType
        {
            None,
            Info,
            Debug,
            Warning,
            Error,
            Halt,
        }

        public static Result Log(string message, MessageType messageType, string callingPackage)
        {
            var messageTypeString = messageType switch
            {
                MessageType.None => "",
                MessageType.Info => "[INFO] ",
                MessageType.Debug => "[DEBUG] ",
                MessageType.Warning => "[WARNING] ",
                MessageType.Error => "[ERROR] ",
                MessageType.Halt => "[SERIOUS ERROR] ",
                _ => ""
            };
            switch (messageTypeString)
            {
                case "[ERROR]":
                    Console.Error.WriteLine("[ERROR] [" + callingPackage + "] " + message);
                    break;
                case "[SERIOUS ERROR]":
                    throw new ApplicationException("[SERIOUS ERROR] [" + callingPackage + "] " + message);
                default:
                    Console.WriteLine(messageTypeString + "[" + callingPackage + "] " + message);
                    break;
            }
            return Result.None;
        }

        public static Result YesNoPrompt(string message, string title, string callingPackage, MessageType type = MessageType.None)
        {
            // TODO: add ability to override to allow use on different UI implementations (Win Forms, Avalonia UI, etc.)
            var messageTypeString = type switch
            {
                MessageType.None => "",
                MessageType.Info => "[INFO] ",
                MessageType.Debug => "[DEBUG] ",
                MessageType.Warning => "[WARNING] ",
                MessageType.Error => "[ERROR] ",
                MessageType.Halt => "[SERIOUS ERROR] ",
                _ => ""
            };
            Console.WriteLine(messageTypeString + "[" + callingPackage + "]  (" + title + ") " + message + " Y/N");
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
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(key.ToString(), "Invalid key pressed");
                }
            }
        }

        public static Result OkPrompt(string message, string title, string callingPackage, MessageType type = MessageType.None)
        {
            // TODO: add ability to override to allow use on different UI implementations (Win Forms, Avalonia UI, etc.)
            var messageTypeString = type switch
            {
                MessageType.None => "",
                MessageType.Info => "[INFO] ",
                MessageType.Debug => "[DEBUG] ",
                MessageType.Warning => "[WARNING] ",
                MessageType.Error => "[ERROR] ",
                MessageType.Halt => "[SERIOUS ERROR] ",
                _ => ""
            };
            Console.WriteLine(messageTypeString + "[" + callingPackage + "]  (" + title + ") " + message);
            Console.WriteLine("Press Any Key To Continue");
            while (true)
            {
                while (Console.KeyAvailable == false)
                    Thread.Sleep(250); // Loop until input is entered.

                Console.ReadKey();
                return Result.Ok;
            }
        }
    }
    public static class Fs
    {
        public static void CopyFiles(string sourceFolder, string targetFolder, bool debugMode)
        {
            Directory.CreateDirectory(targetFolder);
            foreach (var directory in Directory.GetDirectories(sourceFolder))
            {
                var newDirectory = Path.Combine(targetFolder, Path.GetFileName(directory));
                Directory.CreateDirectory(newDirectory);
                CopyFiles(directory, newDirectory, debugMode);
            }

            foreach (var file in Directory.GetFiles(sourceFolder))
            {
                if (debugMode)
                {
                    ConsoleUi.OkPrompt(file, "File", "jLib");
                }

                File.Copy(file, Path.Combine(targetFolder, Path.GetFileName(file)), true);
            }
        }

        public static void DeleteDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                ConsoleUi.OkPrompt(dir, "Removing directory:", "jLib", ConsoleUi.MessageType.Info);
                Directory.Delete(dir, true);
            }
            else
            {
                ConsoleUi.OkPrompt(dir, "Directory does not exist:", "jLib", ConsoleUi.MessageType.Error);
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
        public static string? ConfigFolderName { get; private set; }

        public static void CheckConfigFolderName()
        {
            if (ConfigFolderName == null)
            {
                ConsoleUi.Log("ConfigFolderName has not been set, please use SetConfigFolderName() when the app is starting!!",
                    ConsoleUi.MessageType.Halt, 
                    "jLib");
            }
        }

        public static void SetConfigFolderName(string configFolderName)
        {
            ConfigFolderName = configFolderName;
        }

        public static void WriteBoolToConfig(string key, bool value)
        {
            CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var baseKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName!, true) ??
                                    Registry.CurrentUser.CreateSubKey(ConfigFolderName!);
                baseKey.SetValue(key, value ? 1 : 0, RegistryValueKind.DWord);
            }
            else
            {
                ConsoleUi.OkPrompt("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", "jLib", ConsoleUi.MessageType.Error); // TODO: implement on linux
            }
        }

        public static bool ReadConfigBool(string key)
        {
            CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var configKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName!);
                if (configKey == null) return false;
                var value = configKey.GetValue(key);
                return value != null && (int)value != 0;

            }
            else
            {
                ConsoleUi.OkPrompt("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", "jLib", ConsoleUi.MessageType.Error); // TODO: implement on linux

                return false;
            }
        }

        public static void WriteStringToRegistry(string key, string value)
        {
            CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var baseKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName!, true) ??
                                    Registry.CurrentUser.CreateSubKey(ConfigFolderName!);
                baseKey.SetValue(key, value, RegistryValueKind.String);
            }
            else
            {
                ConsoleUi.OkPrompt("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", "jLib", ConsoleUi.MessageType.Error); // TODO: implement on linux
            }
        }

        public static string ReadRegistryString(string key)
        {
            CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var configKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName!);
                if (configKey != null)
                {
                    return configKey.GetValue(key) as string ?? string.Empty;
                }

                return string.Empty;
            }
            else
            {
                ConsoleUi.OkPrompt("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", "jLib", ConsoleUi.MessageType.Error); // TODO: implement on linux

                return string.Empty;
            }
        }
    }

    public static class DebugMode
    {
        public static bool GetDebugMode()
        {
            Config.CheckConfigFolderName();
            const string jLibConfigSubKey = @"\Config\jLib"; 
            var configKeyLocation = Config.ConfigFolderName + jLibConfigSubKey; 
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var configKey = Registry.CurrentUser.OpenSubKey(configKeyLocation);
                var value = configKey?.GetValue("DebugMode"); 
                if (value is int i) 
                { 
                    return i != 0; 
                }

                return false;
            }
            else
            {
                ConsoleUi.OkPrompt("Not Implemented Yet on Linux and UNIX...", "DEBUG Mode Not Implemented", "jLib", ConsoleUi.MessageType.Error); // TODO: implement on linux
                return false;
            }
        }

        public static void SetDebugMode(bool value)
        {
            Config.CheckConfigFolderName();
            const string jLibConfigSubKey = @"\Config\jLib";
            var configKeyLocation = Config.ConfigFolderName + jLibConfigSubKey;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var baseKey = Registry.CurrentUser.CreateSubKey(configKeyLocation, true);
                var completed = value ? 1 : 0;
                baseKey.SetValue("DebugMode", completed, RegistryValueKind.DWord);
            }
            else
            {
                ConsoleUi.OkPrompt("Not Implemented Yet on Linux and UNIX...", "DEBUG Mode Not Implemented", "jLib", ConsoleUi.MessageType.Error); // TODO: implement on linux
            }
        }
    }
}
