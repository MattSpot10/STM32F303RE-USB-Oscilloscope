#include <windows.h>
#include <iostream>

#define WM_DEVICECHANGE 0x0219
#define DBT_DEVICEARRIVAL 0x8000
#define DBT_DEVICEREMOVECOMPLETE 0x8004

// Function to handle the device change event
void HandleDeviceArrival() {
    std::cout << "USB Device Plugged In!" << std::endl;
    // Your code to execute on USB plug-in event
}

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    if (uMsg == WM_DEVICECHANGE) {
        if (wParam == DBT_DEVICEARRIVAL) {
            HandleDeviceArrival();
        }
    }
    return DefWindowProc(hwnd, uMsg, wParam, lParam);
}

int main() {
    WNDCLASS wc = {0};
    wc.lpfnWndProc = WindowProc;
    wc.hInstance = GetModuleHandle(NULL);
    wc.lpszClassName = L"USBEventListener";

    if (!RegisterClass(&wc)) {
        std::cerr << "Window class registration failed!" << std::endl;
        return 1;
    }

    HWND hwnd = CreateWindow(wc.lpszClassName, L"USB Event Listener", WS_OVERLAPPEDWINDOW,
                             CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
                             NULL, NULL, wc.hInstance, NULL);

    if (!hwnd) {
        std::cerr << "Window creation failed!" << std::endl;
        return 1;
    }

    // Enter the message loop
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return 0;
}