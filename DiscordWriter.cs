using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Keyboard
{
    public class DiscordWriter
    {
        const UInt32 WM_CHAR = 0x0102;
        const UInt32 WM_KEYDOWN = 0x0100;
        bool shouldRun;
        Thread writerThread;

        public int SleepTime { get; set; }
        public bool ShouldAutoSend { get; set; }
        public int AutoSendCount { get; set; }
        public string WriterText { get; set; }

        public DiscordWriter()
        {
            SleepTime = 1;
            WriterText = "e";
            ShouldAutoSend = false;
            AutoSendCount = 100;
        }

        public void Start()
        {
            writerThread = new Thread(Write);
            writerThread.Start();
        }

        public void Stop()
        {
            shouldRun = false;
        }

        public bool SetText(string pText)
        {
            WriterText = pText;
            if (WriterText.Length > 0) return true;
            WriterText = "e";
            return false;
        }

        public bool SetTextWithSpaces(string[] pText)
        {
            WriterText = "";
            foreach (string str in pText)
            {
                WriterText += str + " ";
            }
            if (WriterText.Length > 0) return true;
            WriterText = "e";
            return false;
        }

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        void Write()
        {
            shouldRun = true;
            int loopCount = 0;
            while (shouldRun)
            {
                if (loopCount < AutoSendCount)
                {
                    Process[] processes = Process.GetProcessesByName("Discord");
                    foreach (Process proc in processes)
                    {
                        foreach (char c in WriterText)
                        {
                            PostMessage(proc.MainWindowHandle, WM_CHAR, c, 0);
                        }
                    }
                    Thread.Sleep(SleepTime);
                    loopCount++;
                }
                else if (ShouldAutoSend)
                {
                    loopCount = 0;
                    Process[] processes = Process.GetProcessesByName("Discord");
                    foreach (Process proc in processes)
                    {
                        PostMessage(proc.MainWindowHandle, WM_KEYDOWN, 0x0D, 0); // 0x0D => Enter-Key
                    }
                    Thread.Sleep(SleepTime);
                }
            }
        }
    }
}