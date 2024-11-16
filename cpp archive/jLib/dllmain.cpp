// common imports
#include <windows.h>

// dllmain.cpp : Defines the entry point for the DLL application.
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
        MessageBoxW(NULL, L"jLib v1.2 cpp", L"jLib info", MB_OK | MB_ICONINFORMATION);
        break;
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}


