﻿//Created by Ryan Hatfield and Brandon Cuddigan
// TopHatHacker
// Free to use, just don't tell me about it. I really don't care.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace Macro5000
{
    class Program
    {
        //requred import for bringing window to the front
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const byte VK_LSHIFT = 0xA0; // left shift key
        public const byte VK_TAB = 0x09; //tab key
        public const int KEYEVENTF_EXTENDEDKEY = 0x01;
        public const int KEYEVENTF_KEYUP = 0x02;

        static void Main(string[] args)
        {
            string tempArgument = "\"C:\\Documents and Settings\\administrator.SLEEPINN\\Desktop\\CameraStatus.lxd\"";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "/lxd")
                {
                    if (i + 1 < args.Length)
                        tempArgument = args[i + 1];
                }
            }

            foreach (Process proc in Process.GetProcessesByName("camerastatusreport"))
            {
                proc.Kill();
            }

            foreach (Process proc in Process.GetProcessesByName("iexplore"))
            {
                proc.Kill();
            }

            Process process = new Process();
            process.StartInfo.FileName = "C:\\Program Files\\Luxriot Digital Video Recorder\\CameraStatusReport.exe";
            process.StartInfo.Arguments = tempArgument + " -check";
            process.Start();

            //make sure the window has focus
            SetForegroundWindow(process.MainWindowHandle);

            //wait 3 second (3000 milliseconds)
            System.Threading.Thread.Sleep(3000);

            SendKeys.SendWait("%c");
            SendKeys.SendWait("s");
            //shift tab would be something like
            System.Threading.Thread.Sleep(2000);
            SendKeys.SendWait("+{TAB}");
            SendKeys.SendWait("{ENTER}");
            for(int i=0; i<900; i++)
                System.Threading.Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(10000);

            foreach (Process proc in Process.GetProcessesByName("camerastatusreport"))
            {
                proc.Kill();
            }
        }

    }
}