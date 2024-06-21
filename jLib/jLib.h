#pragma once
#include <windows.h>
#include <filesystem>
#include <string>

#ifdef JLIB_EXPORTS
#define JLIB_API __declspec(dllexport)
#else
#define JLIB_API __declspec(dllimport)
#endif

namespace jLib
{
    namespace Registry
    {
        void JLIB_API CheckAppKey();
        void JLIB_API SetAppKey(LPCWSTR key);
        // Wites Boolean value to windows registry
        void JLIB_API Write_bool_to_Registry(LPCWSTR key, bool value);

        // Returns boolean value from windows registry
        bool JLIB_API ReadRegistryBool(LPCWSTR key);

        // Writes LPCWSTR string to windows registry
        void JLIB_API Write_string_to_Registry(LPCWSTR key, LPCWSTR value);

        // Returns std::string from windows registry
        std::string JLIB_API ReadRegistryString(LPCWSTR key);
    }

    namespace Dialog
    {
        int JLIB_API YesNoDialog(LPCWSTR message, LPCWSTR title, int icon, bool debugMode);

        int JLIB_API OKDialog(LPCWSTR message, LPCWSTR title, int icon, bool debugMode);
    }

    namespace FS
    {
        void JLIB_API CopyFiles(const std::string& sourceFolder, const std::string& targetFolder, bool debugMode);

        void JLIB_API DeleteDirectory(const std::string& dir);

        std::string JLIB_API GetUserFolder();

        std::string JLIB_API GetAppDataFolder();

        void JLIB_API CreateDirectory(const std::string& dir);
    }

    namespace DebugMode
    {
        bool JLIB_API GetDebugMode(const wchar_t appKey);

        void JLIB_API SetDebugMode(bool value);
    }
}
