#include <windows.h>
#include <dbt.h>
#include <iostream>
#include <initguid.h>
#include <ntddstor.h>
#include <setupapi.h>
#include <devguid.h>
#include <usb100.h>



#include <tchar.h>

#include <string>
#include <memory>



#define WM_DEVICECHANGE 0x0219
#define DBT_DEVICEARRIVAL 0x8000
#define DBT_DEVICEREMOVECOMPLETE 0x8004
#define STM32_VID 0x0483     // Vendor ID for STM32
#define STM32F303_PID 0x5740 // Example Product ID for STM32F303 (change if necessary)


HANDLE hProcess = NULL;  // Global variable to store the process handle

// Function to start the process using CreateProcess
void StartProcess()
{
    // Path to your application
    const wchar_t* exePath = L"C:\\Program Files (x86)\\EMBO\\EMBO.exe";

    // Setup startup information and process information structures
    STARTUPINFO si = { 0 };
    PROCESS_INFORMATION pi = { 0 };
    
    si.cb = sizeof(STARTUPINFO);

    // Create the process
    if (!CreateProcess(
            exePath,               // Path to executable
            NULL,                  // Command line (use NULL for default)
            NULL,                  // Process security attributes
            NULL,                  // Thread security attributes
            FALSE,                 // Inherit handles
            0,                     // Creation flags (0 for default)
            NULL,                  // Environment (use NULL for default)
            NULL,                  // Current directory (use NULL for default)
            &si,                   // STARTUPINFO structure
            &pi                    // PROCESS_INFORMATION structure to receive process info
        ))
    {
        std::wcout << L"Failed to launch process. Error: " << GetLastError() << std::endl;
    }
    else
    {
        // Save the process handle
        hProcess = pi.hProcess;
        std::wcout << L"Process launched successfully with PID: " << pi.dwProcessId << std::endl;
    }
}

// Function to terminate the process
void KillProcess()
{
    if (hProcess != NULL)
    {
        // Terminate the process if it is running
        if (TerminateProcess(hProcess, 0))
        {
            std::wcout << L"Process terminated successfully." << std::endl;
        }
        else
        {
            std::wcout << L"Failed to terminate process. Error: " << GetLastError() << std::endl;
        }
        CloseHandle(hProcess);
        hProcess = NULL;
    }
}



LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    if (uMsg == WM_DEVICECHANGE)
    {
        // Check if the device change is related to a port (serial device)
        if (wParam == DBT_DEVICEARRIVAL)
        {
            // Cast the lParam to a DEV_BROADCAST_HDR pointer
            PDEV_BROADCAST_HDR pHdr = (PDEV_BROADCAST_HDR)lParam;
            
            // Check if it's a port device
            if (pHdr->dbch_devicetype == DBT_DEVTYP_PORT)
            {
                // Cast to DEV_BROADCAST_PORT to access the COM port name
                PDEV_BROADCAST_PORT pPort = (PDEV_BROADCAST_PORT)pHdr;

                // Print out the COM port name
                // pPort->dbcp_name contains the port name (e.g., "COM3")
                std::wcout << L"Device connected at: " << pPort->dbcp_name << std::endl;
                if (wcsstr(pPort->dbcp_name, L"COM18") != nullptr)
                {
                    std::wcout << L"Device connected to COM18!" << std::endl;
                    StartProcess();
                }
            }
        }
        if (wParam == DBT_DEVICEREMOVECOMPLETE) {
            // Cast the lParam to a DEV_BROADCAST_HDR pointer
            PDEV_BROADCAST_HDR pHdr = (PDEV_BROADCAST_HDR)lParam;
            
            // Check if it's a port device
            if (pHdr->dbch_devicetype == DBT_DEVTYP_PORT)
            {
                // Cast to DEV_BROADCAST_PORT to access the COM port name
                PDEV_BROADCAST_PORT pPort = (PDEV_BROADCAST_PORT)pHdr;

                // Print out the COM port name
                // pPort->dbcp_name contains the port name (e.g., "COM3")
                std::wcout << L"Device disconnected at: " << pPort->dbcp_name << std::endl;
                if (wcsstr(pPort->dbcp_name, L"COM18") != nullptr)
                {
                    std::wcout << L"Device connected to COM18!" << std::endl;
                    KillProcess();
                }
            }
        }
    }

    // Default window processing
    return DefWindowProc(hwnd, uMsg, wParam, lParam);
}


int main()
{
    WNDCLASS wc = {0};
    wc.lpfnWndProc = WindowProc;
    wc.hInstance = GetModuleHandle(NULL);
    wc.lpszClassName = L"USBEventListener";

    if (!RegisterClass(&wc))
    {
        std::cerr << "Window class registration failed!" << std::endl;
        return 1;
    }

    HWND hwnd = CreateWindow(wc.lpszClassName, L"USB Event Listener", WS_OVERLAPPEDWINDOW,
                             CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
                             NULL, NULL, wc.hInstance, NULL);

    if (!hwnd)
    {
        std::cerr << "Window creation failed!" << std::endl;
        return 1;
    }

    // Enter the message loop
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return 0;
}