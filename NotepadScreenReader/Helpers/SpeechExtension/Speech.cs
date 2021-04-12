using System;
using System.IO;
using System.Text;
using System.Threading;

namespace NotepadScreenReader.Helpers.SpeechExtension
{
    internal static class Speech
    {
        internal static void Speak(string textToSpeech, bool wait = false)  
        {  
            // Command to execute PS  
            Execute($@"Add-Type -AssemblyName System.speech;  
            $speak = New-Object System.Speech.Synthesis.SpeechSynthesizer;                           
            $speak.Speak(""{textToSpeech}"");"); // Embedd text  
  
            void Execute(string command)  
            {  
                // create a temp file with .ps1 extension  
                var cFile = System.IO.Path.GetTempPath() + Guid.NewGuid() + ".ps1";  
  
                //Write the .ps1  
                using var tw = new System.IO.StreamWriter(cFile, false, Encoding.UTF8);  
                tw.Write(command);  
  
                // Setup the PS  
                var start =  
                    new System.Diagnostics.ProcessStartInfo()  
                    {  
                        FileName = "C:\\windows\\system32\\windowspowershell\\v1.0\\powershell.exe",                
                        LoadUserProfile = false,  
                        UseShellExecute = false,  
                        CreateNoWindow = true,  
                        Arguments = $"-executionpolicy bypass -File {cFile}",  
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden  
                    };  
  
                //Init the Process  
                using var p = System.Diagnostics.Process.Start(start) ??
                              throw new ArgumentNullException($"Cant run powershell {cFile} speech process");
                
                //if (wait) p.WaitForExit(); оно не работает, разобраться почему не успел, поэтому сделана очень топорная приостановка процесса. 
                if (wait) Thread.Sleep(120);
            }  
        }  
    }
}