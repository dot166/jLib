using System.Runtime.InteropServices;
using Microsoft.Win32;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace jLib
{
    public static class Dialog
    {

        public static async Task<ButtonResult> YesNoDialog(string message, string title, Icon icon)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard(title, message,
                    ButtonEnum.YesNo, icon);

            var result = await box.ShowAsync(); 
            switch (result) 
            { 
                case ButtonResult.No: 
                    return ButtonResult.No;
                case ButtonResult.Yes: 
                    return ButtonResult.Yes;
                case ButtonResult.Ok: 
                case ButtonResult.Abort:
                case ButtonResult.Cancel:
                case ButtonResult.None:
                default:
                    await MessageBoxManager.GetMessageBoxStandard("Return", "WHAT DID U DO!!!!").ShowAsync();
                    return result;
            }
        }

        public static async Task<ButtonResult> OkDialog(string message, string title, Icon icon)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard(title, message,
                    ButtonEnum.Ok, icon);

            var result = await box.ShowAsync();
            if (result == ButtonResult.Ok)
            {
                return ButtonResult.Ok;
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Return", "WHAT DID U DO!!!!").ShowAsync();
                   return result;
            }
        }
    }

    namespace FS
    {
        public static class FileSystem
        {
            public static async Task CopyFiles(string sourceFolder, string targetFolder, bool debugMode)
            {
                Directory.CreateDirectory(targetFolder);
                foreach (var directory in Directory.GetDirectories(sourceFolder))
                {
                    var newDirectory = Path.Combine(targetFolder, Path.GetFileName(directory));
                    Directory.CreateDirectory(newDirectory);
                    await CopyFiles(directory, newDirectory, debugMode);
                }

                foreach (var file in Directory.GetFiles(sourceFolder))
                {
                    if (debugMode)
                    {
                        await Dialog.OkDialog(file, "File", 0);
                    }

                    File.Copy(file, Path.Combine(targetFolder, Path.GetFileName(file)), true);
                }
            }

            public static async Task DeleteDirectory(string dir)
            {
                if (Directory.Exists(dir))
                {
                    await Dialog.OkDialog(dir, "Removing directory:", Icon.Info);
                    Directory.Delete(dir, true);
                }
                else
                {
                    await Dialog.OkDialog(dir, "Directory does not exist:", Icon.Error);
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
    }

    public static class Config
    {
        public static string ConfigFolderName { get; private set; } = null!;

        public static async Task CheckConfigFolderName()
        {
            if (ConfigFolderName == null)
            {
                await Dialog.OkDialog("ERROR: ConfigFolderName has not been set, Please contact developer",
                    "jOS Error Handling", 
                    Icon.Error);
                throw new ArgumentException(
                    "ConfigFolderName has not been set, please use SetConfigFolderName() when the app is starting!!");
            }
        }

        public static void SetConfigFolderName(string configFolderName)
        {
            ConfigFolderName = configFolderName;
        }

        public static async Task WriteBoolToConfig(string key, bool value)
        {
            await CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var baseKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName, true) ??
                                    Registry.CurrentUser.CreateSubKey(ConfigFolderName);
                baseKey.SetValue(key, value ? 1 : 0, RegistryValueKind.DWord);
            }
            else
            {
                await Dialog.OkDialog("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", Icon.Error);
            }
        }

        public static async Task<bool> ReadConfigBool(string key)
        {
            await CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var configKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName);
                if (configKey == null) return false;
                var value = configKey.GetValue(key);
                return value != null && (int)value != 0;

            }
            else
            {
                await Dialog.OkDialog("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", Icon.Error);

                return false;
            }
        }

        public static async Task WriteStringToRegistry(string key, string value)
        {
            await CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var baseKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName, true) ??
                                    Registry.CurrentUser.CreateSubKey(ConfigFolderName);
                baseKey.SetValue(key, value, RegistryValueKind.String);
            }
            else
            {
                await Dialog.OkDialog("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", Icon.Error);
            }
        }

        public static async Task<string> ReadRegistryString(string key)
        {
            await CheckConfigFolderName();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var configKey = Registry.CurrentUser.OpenSubKey(ConfigFolderName);
                if (configKey != null)
                {
                    return configKey.GetValue(key) as string ?? string.Empty;
                }

                return string.Empty;
            }
            else
            {
                await Dialog.OkDialog("Not Implemented Yet on Linux and UNIX...", "Config Values Not Implemented", Icon.Error);

                return string.Empty;
            }
        }
    }

    public static class DebugMode
    {
        public static async Task<bool> GetDebugMode()
        {
            await Config.CheckConfigFolderName();
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
                await Dialog.OkDialog("Not Implemented Yet on Linux and UNIX...", "DEBUG Mode Not Implemented", Icon.Error);
                return false;
            }
        }

        public static async Task SetDebugMode(bool value)
        {
            await Config.CheckConfigFolderName();
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
                await Dialog.OkDialog("Not Implemented Yet on Linux and UNIX...", "DEBUG Mode Not Implemented", Icon.Error);
            }
        }
    }
}
