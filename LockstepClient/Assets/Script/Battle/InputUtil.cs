using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputUtil
{

    public static bool MouseCanSee = true;

    public static void HideMouse()
    {
        MouseCanSee = false;
        ChangeMouseState(false, true);
    }

    public static void ShowMouse()
    {
        MouseCanSee = true;
        ChangeMouseState(true, false);
    }

    public static void ChangeMouseState(bool show, bool lockState)
    {
        Cursor.visible = show;
        Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
