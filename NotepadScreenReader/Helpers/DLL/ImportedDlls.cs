using System;
using System.Runtime.InteropServices;
using System.Windows;
using Point = NotepadScreenReader.Helpers.Structs.Point;
using Rect = NotepadScreenReader.Helpers.Structs.Rect;

namespace NotepadScreenReader.Helpers.DLL
{
    internal static class ImportedDlls
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetMenu(IntPtr hwndParent);
        

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetSubMenu(IntPtr hMenu, int nPos);
        

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetMenuItemID(IntPtr nMenu, int nPos);
        

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint uItem, out Rect lprcItem);
        

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr CheckMenuItem(IntPtr hMenu, uint uIdCheckItem, uint uCheck);
        

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetCursorPos(out Point lpPoint);
    }
}