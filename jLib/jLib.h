#pragma once
#include <windows.h>
#include <filesystem>
#include <string>

namespace jLib
{
    class Registry
    {
    public:
        // Wites Boolean value to windows registry
        static void Write_bool_to_Registry(LPCWSTR subkey, LPCWSTR key, bool value);

        // Returns boolean value from windows registry
        static bool ReadRegistryBool(LPCWSTR subkey, LPCWSTR key);

        // Writes LPCWSTR string to windows registry
        static void Write_string_to_Registry(LPCWSTR subkey, LPCWSTR key, LPCWSTR value);

        // Returns std::string from windows registry
        static std::string ReadRegistryString(LPCWSTR subkey, LPCWSTR key);
    };

    class Dialog
    {
    public:
        static int YesNoDialog(LPCWSTR message, LPCWSTR title, int icon, bool debugMode);

        static int OKDialog(LPCWSTR message, LPCWSTR title, int icon, bool debugMode);
    };

    class FS
    {
    public:
        static void CopyFiles(const std::string& sourceFolder, const std::string& targetFolder, bool debugMode);

        static void DeleteDirectory(const std::string& dir);

        static std::string GetUserFolder();

        static std::string GetAppDataFolder();

        static void CreateDirectory(const std::string& dir);
    };
}
