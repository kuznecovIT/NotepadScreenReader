using System;
using System.Collections.Generic;
using System.Threading;
using NotepadScreenReader.Helpers.Enumerations;
using NotepadScreenReader.Helpers.LogMessages;
using NotepadScreenReader.Helpers.SpeechExtension;
using NotepadScreenReader.Helpers.Structs;
using static NotepadScreenReader.Helpers.DLL.ImportedDlls;


namespace NotepadScreenReader.Helpers
{
    internal static class ScreenReader
    {
        internal static void StartScreenReader()
        {
            var notepadApplicationWindow = GetNotepadApplicationWindow();

            while (notepadApplicationWindow != IntPtr.Zero)
            {
                var notepadMenu = GetMenu(notepadApplicationWindow);
                
                var listOfNotepadMenuItemsRects = GetListOfNotepadMenuItemsRects(notepadApplicationWindow, notepadMenu);

                GetCursorPos(out var mouseCurrentPosition);

                PrintNotepadMenuItemToLogIfSelected(listOfNotepadMenuItemsRects, mouseCurrentPosition);
                SayNotepadMenuNameIfSelected(listOfNotepadMenuItemsRects, mouseCurrentPosition);
                
                Thread.Sleep(200);
                
                //scan for notepad still exist
                notepadApplicationWindow = FindWindow("notepad", null);
            }
            
            Console.WriteLine(ServiceMessages.NotepadApplicationsClosed);
        }

        private static IntPtr GetNotepadApplicationWindow()
        {
            Console.WriteLine(ServiceMessages.SearchingForNotepadApplication);
            
            var notepadWindow = IntPtr.Zero;
            while (notepadWindow == IntPtr.Zero)
            {
                notepadWindow = FindWindow("notepad", null);
            }

            Console.WriteLine(ServiceMessages.NotepadApplicationFound);
            return notepadWindow;
        }


        private static List<Rect> GetListOfNotepadMenuItemsRects(IntPtr notepadApplicationWindow, IntPtr notepadMenu)
        {
            GetMenuItemRect(notepadApplicationWindow, notepadMenu, (int)NotepadMenuItems.File, out var filePositionRect);
            GetMenuItemRect(notepadApplicationWindow, notepadMenu, (int)NotepadMenuItems.Edit, out var editPositionRect);
            GetMenuItemRect(notepadApplicationWindow, notepadMenu, (int)NotepadMenuItems.Format, out var formatPositionRect);
            GetMenuItemRect(notepadApplicationWindow, notepadMenu, (int)NotepadMenuItems.View, out var viewPositionRect);
            GetMenuItemRect(notepadApplicationWindow, notepadMenu, (int)NotepadMenuItems.Help, out var helpPositionRect);
            
            return new List<Rect>()
                {filePositionRect, editPositionRect, formatPositionRect, viewPositionRect, helpPositionRect};
        }
        
        private static void PrintNotepadMenuItemToLogIfSelected(IList<Rect> listOfNotepadMenuItemsRects, Point mouseCurrentPosition)
        {
            foreach (var position in listOfNotepadMenuItemsRects)
            {
                if (!position.Contains(mouseCurrentPosition)) continue;
                var indexOfPosition = listOfNotepadMenuItemsRects.IndexOf(position);
                Console.WriteLine(Enum.GetName(typeof(NotepadMenuItems), indexOfPosition));
            }
        }

        private static void SayNotepadMenuNameIfSelected(IList<Rect> listOfNotepadMenuItemsRects, Point mouseCurrentPosition)
        {
            foreach (var position in listOfNotepadMenuItemsRects)
            {
                if (!position.Contains(mouseCurrentPosition)) continue;
                var indexOfPosition = listOfNotepadMenuItemsRects.IndexOf(position);
                
                Speech.Speak(Enum.GetName(typeof(NotepadMenuItems), indexOfPosition), true);
            }
        }
    }
}