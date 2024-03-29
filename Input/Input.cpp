#include <windows.h>

extern "C" __declspec(dllexport) void __stdcall SimulateKeyDown(int keyCode, bool isShift, bool isCtrl, bool isAlt);
extern "C" __declspec(dllexport) void __stdcall SimulateKeyUp(int keyCode, bool isShift, bool isCtrl, bool isAlt);


void SimulateKeyDown(int keyCode, bool isShift, bool isCtrl, bool isAlt)
{
	int arraySize = 0;
	INPUT* inputs = NULL;

	//key down
	if (isShift)
	{
		arraySize++;
		inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
		inputs[arraySize - 1].type = INPUT_KEYBOARD;
		inputs[arraySize - 1].ki.wVk = VK_SHIFT;
		inputs[arraySize - 1].ki.dwFlags = NULL;
	}

	if (isCtrl)
	{
		arraySize++;
		inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
		inputs[arraySize - 1].type = INPUT_KEYBOARD;
		inputs[arraySize - 1].ki.wVk = VK_CONTROL;
		inputs[arraySize - 1].ki.dwFlags = NULL;

	}

	if (isAlt)
	{
		arraySize++;
		inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
		inputs[arraySize - 1].type = INPUT_KEYBOARD;
		inputs[arraySize - 1].ki.wVk = VK_MENU;
		inputs[arraySize - 1].ki.dwFlags = NULL;

	}

	arraySize++;
	inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
	inputs[arraySize - 1].type = INPUT_KEYBOARD;
	inputs[arraySize - 1].ki.wVk = keyCode;
	inputs[arraySize - 1].ki.dwFlags = NULL;


	SendInput(arraySize, inputs, sizeof(INPUT));

	free(inputs);
}

void SimulateKeyUp(int keyCode, bool isShift, bool isCtrl, bool isAlt)
{
	int arraySize = 0;
	INPUT* inputs = NULL;

	arraySize++;
	inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
	inputs[arraySize - 1].type = INPUT_KEYBOARD;
	inputs[arraySize - 1].ki.wVk = keyCode;
	inputs[arraySize - 1].ki.dwFlags = KEYEVENTF_KEYUP;

	if (isShift)
	{
		arraySize++;
		inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
		inputs[arraySize - 1].type = INPUT_KEYBOARD;
		inputs[arraySize - 1].ki.wVk = VK_SHIFT;
		inputs[arraySize - 1].ki.dwFlags = KEYEVENTF_KEYUP;
	}

	if (isCtrl)
	{
		arraySize++;
		inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
		inputs[arraySize - 1].type = INPUT_KEYBOARD;
		inputs[arraySize - 1].ki.wVk = VK_CONTROL;
		inputs[arraySize - 1].ki.dwFlags = KEYEVENTF_KEYUP;

	}

	if (isAlt)
	{
		arraySize++;
		inputs = (INPUT*)realloc(inputs, sizeof(INPUT) * arraySize);
		inputs[arraySize - 1].type = INPUT_KEYBOARD;
		inputs[arraySize - 1].ki.wVk = VK_MENU;
		inputs[arraySize - 1].ki.dwFlags = KEYEVENTF_KEYUP;

	}


	SendInput(arraySize, inputs, sizeof(INPUT));

	free(inputs);
}




