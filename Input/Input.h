#pragma once
#include "windows.h">

extern "C" __declspec(dllexport) void __stdcall SimulateKeyDown(int keyCode, bool isShift, bool isCtrl, bool isAlt);
extern "C" __declspec(dllexport) void __stdcall SimulateKeyUp(int keyCode, bool isShift, bool isCtrl, bool isAlt);

