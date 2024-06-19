#include "jLib.h"

//MessageBoxW(NULL, L"jLib v1.1 cpp", L"jLib info", MB_OK | MB_ICONINFORMATION);

namespace jLib
{
    int Dialog::YesNoDialog(LPCWSTR message, LPCWSTR title, int icon, bool debugMode) {
        int dingus = MessageBox(NULL, message, title, MB_YESNO | icon);
        if (dingus == IDNO) {
            wchar_t buffer[256];
            wsprintfW(buffer, L"%d", IDNO);
            if (debugMode) { MessageBox(NULL, buffer, L"Return", MB_OK); }
            return IDNO;
        }
        else if (dingus == IDYES) {
            wchar_t buffer[256];
            wsprintfW(buffer, L"%d", IDYES);
            if (debugMode) { MessageBox(NULL, buffer, L"Return", MB_OK); }
            return IDYES;
        }
        else {
            MessageBox(NULL, L"WHAT DID U DO!!!!", L"Return", MB_OK);
            return IDCANCEL;
        }
    }

    int Dialog::OKDialog(LPCWSTR message, LPCWSTR title, int icon, bool debugMode) {
        int dingus = MessageBoxW(NULL, message, title, MB_OK | icon);
        if (dingus == IDOK) {
            wchar_t buffer[256];
            wsprintfW(buffer, L"%d", IDOK);
            if (debugMode) { MessageBoxW(NULL, buffer, L"Return", MB_OK); }
            return IDOK;
        }
        else {
            MessageBoxW(NULL, L"WHAT DID U DO!!!!", L"Return", MB_OK);
            return IDCANCEL;
        }
    }


    namespace fs = std::filesystem;

    void FS::CopyFiles(const std::string& sourceFolder, const std::string& targetFolder, bool debugMode) {
        fs::create_directory(targetFolder);
        for (const auto& directory : fs::directory_iterator(sourceFolder)) {
            if (fs::is_directory(directory)) {
                // Get the path of the new directory
                std::string newDirectory = (fs::path(targetFolder) / directory.path().filename()).string();
                // Create the directory if it doesn't already exist
                fs::create_directory(newDirectory);
                // Recursively clone the directory
                CopyFiles(directory.path().string(), newDirectory, debugMode);
            }
        }

        for (const auto& file : fs::directory_iterator(sourceFolder)) {
            if (fs::is_regular_file(file)) {
                if (debugMode) {
                    std::wstring stemp = std::wstring(file.path().string().begin(), file.path().string().end());
                    LPCWSTR sw = stemp.c_str();
                    jLib::Dialog::OKDialog(std::wstring(file.path().string().begin(), file.path().string().end()).c_str(), L"File", NULL, false);
                    //std::cout << "File: " << file.path().string() << std::endl;
                }
                fs::copy_file(file, fs::path(targetFolder) / file.path().filename(), fs::copy_options::overwrite_existing);
            }
        }
    }

    void FS::DeleteDirectory(const std::string& dir) {
        if (fs::exists(dir) && fs::is_directory(dir)) {
            jLib::Dialog::OKDialog(std::wstring(dir.begin(), dir.end()).c_str(), L"Removing directory:", MB_ICONINFORMATION, false);
            //std::cout << "Removing directory: " << dir << std::endl;
            fs::remove_all(dir);
        }
        else {
            jLib::Dialog::OKDialog(std::wstring(dir.begin(), dir.end()).c_str(), L"Directory does not exist:", MB_ICONERROR, false);
            //std::cout << "Directory does not exist: " << dir << std::endl;
        }
    }

    std::string FS::GetUserFolder() {
        char* pValue;
        size_t len;
        _dupenv_s(&pValue, &len, "USERPROFILE");
        return pValue;
    }

    std::string FS::GetAppDataFolder() {
        char* pValue;
        size_t len;
        _dupenv_s(&pValue, &len, "APPDATA");
        return pValue;
    }

    void FS::CreateDirectory(const std::string& dir) {
        DeleteDirectory(dir);
        fs::create_directory(dir);
    }



    void Registry::Write_bool_to_Registry(LPCWSTR subkey, LPCWSTR key, bool value)
    {
        HKEY baseKey;
        if (RegOpenCurrentUser(KEY_ALL_ACCESS, &baseKey) == ERROR_SUCCESS)
        {
            HKEY subKey;
            if (RegCreateKeyExW(baseKey, subkey, 0, NULL, REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, NULL, &subKey, NULL) == ERROR_SUCCESS)
            {
                DWORD completed = value ? 1 : 0;
                RegSetValueExW(subKey, key, 0, REG_DWORD, reinterpret_cast<BYTE*>(&completed), sizeof(DWORD));
                RegCloseKey(subKey);
            }
            RegCloseKey(baseKey);
        }
    }

    bool Registry::ReadRegistryBool(LPCWSTR subkey, LPCWSTR key)
    {
        HKEY regkey;
        DWORD val;
        DWORD valSize = sizeof(DWORD);
        if (RegOpenCurrentUser(KEY_READ, &regkey) == ERROR_SUCCESS)
        {
            if (RegOpenKeyExW(regkey, subkey, 0, KEY_READ, &regkey) == ERROR_SUCCESS)
            {
                if (RegQueryValueExW(regkey, key, NULL, NULL, reinterpret_cast<BYTE*>(&val), &valSize) == ERROR_SUCCESS)
                {
                    RegCloseKey(regkey);
                    return val != 0;
                }
                RegCloseKey(regkey);
            }
        }
        return false;
    }

    void Registry::Write_string_to_Registry(LPCWSTR subkey, LPCWSTR key, LPCWSTR value)
    {
        HKEY baseKey;
        if (RegOpenCurrentUser(KEY_ALL_ACCESS, &baseKey) == ERROR_SUCCESS)
        {
            HKEY subKey;
            if (RegCreateKeyExW(baseKey, subkey, 0, NULL, REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, NULL, &subKey, NULL) == ERROR_SUCCESS)
            {
                RegSetValueExW(subKey, key, 0, REG_SZ, reinterpret_cast<const BYTE*>(value), static_cast<DWORD>(wcslen(value) + 1));
                RegCloseKey(subKey);
            }
            RegCloseKey(baseKey);
        }
    }

    std::string Registry::ReadRegistryString(LPCWSTR subkey, LPCWSTR key)
    {
        HKEY regkey;
        DWORD valSize = 0;
        if (RegOpenCurrentUser(KEY_READ, &regkey) == ERROR_SUCCESS)
        {
            if (RegOpenKeyExW(regkey, subkey, 0, KEY_READ, &regkey) == ERROR_SUCCESS)
            {
                if (RegQueryValueExW(regkey, key, NULL, NULL, NULL, &valSize) == ERROR_SUCCESS && valSize > 0)
                {
                    char value[1024];
                    if (RegQueryValueExW(regkey, key, NULL, NULL, (LPBYTE)value, &valSize) == ERROR_SUCCESS)
                    {
                        RegCloseKey(regkey);
                        return std::string(value);
                    }
                }
                RegCloseKey(regkey);
            }
        }
        return "";
    }
}


