#include "pch.h"
#include <Windows.h>
#include <cstdio>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C" {

    _declspec(dllexport)void Memory(DWORD pid,void*ptr,int newvalue,SIZE_T*byteswritten)
    {
        HANDLE handle = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, FALSE, pid);

        if (handle!=NULL)
        {
            WriteProcessMemory(handle, ptr, &newvalue, sizeof(newvalue), byteswritten);
            CloseHandle(handle);
        }


    }
    


}
