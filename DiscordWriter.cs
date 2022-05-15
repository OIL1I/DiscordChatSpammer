using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Keyboard
{
    public class DiscordWriter
    {
        const UInt32 WN_KEYDOWN = 0x0100;
        bool shouldRun;
        Thread writerThread;

        public int SleepTime { get; set; }
        public bool ShouldAutoSend { get; set; }
        public int AutoSendCount { get; set; }
        int[] VKeys { get; set; }

        public DiscordWriter()
        {
            SleepTime = 1;
            VKeys = new int[] { 0x45 };
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

        public bool SetKey(string pKeyName)
        {
            int result = KeyCodes.GetValueByName(pKeyName);
            if (result > 0)
            {
                VKeys = new int[] { result };
                return true;
            }
            return false;
        }

        public bool SetText(string pText)
        {
            int[] result = KeyCodes.GetValuesFromText(pText);
            if (result.Length > 0)
            {
                VKeys = result;
                return true;
            }
            return false;
        }

        public bool SetTextWithSpaces(string[] pText)
        {
            System.Collections.Generic.List<int> result = new System.Collections.Generic.List<int>();
            foreach (string str in pText)
            {
                result.AddRange(KeyCodes.GetValuesFromText(str));
                result.Add(((int)KeyCodes.KeyCode.kspace));
            }
            if (result.Count > 0)
            {
                VKeys = result.ToArray();
                return true;
            }
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
                       foreach (int VK in VKeys)
                       {
                           PostMessage(proc.MainWindowHandle, WN_KEYDOWN, VK, 0);
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
                        PostMessage(proc.MainWindowHandle, WN_KEYDOWN, 0x0D, 0); // 0x0D => Enter-Key
                    }
                    Thread.Sleep(SleepTime);
                }
            }
        }
    }
}