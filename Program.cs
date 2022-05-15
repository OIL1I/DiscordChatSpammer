﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Keyboard
{
    static class Programm
    {
        [STAThread]
        static void Main()
        {
            DiscordWriter dw = new DiscordWriter();
            while (true)
            {
                String[] input = Console.ReadLine().Split(' ');
                if (input.Length == 0) continue;
                if (input[0].ToLower() == "help")
                {
                    Console.WriteLine(
                        "Write one of following commands:" +
                        "\nstart -> Starts the writer" +
                        "\nstop -> stops the writer" +
                        "\nexit -> closes the program" +
                        "\nautosend [0/1] -> disable/enables automatic sending" +
                        "\nautosendtime [x > 0] -> sets the time till the message gets send to the given value (hast to be greater 0)" +
                        "\nsetkey [kX] -> sets the key to write (X is the name of the key e.g \'a\'" +
                        "\nsettext [text} -> sets the text to write"+
                        "\nsleep [x > 0] -> sets the time till the next character is written (hast to be greater 0)");
                }
                if (input[0].ToLower() == "start")
                {
                    dw.Start();
                    Console.WriteLine("Started writer...");
                }
                else if (input[0].ToLower() == "stop")
                {
                    dw.Stop();
                    Console.WriteLine("Stoped writer...");
                }
                else if (input[0].ToLower() == "exit")
                {
                    dw.Stop();
                    Console.WriteLine("Press any key to Close...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else if (input[0].ToLower() == "autosend")
                {
                    if (input.Length < 2) continue;
                    if (int.TryParse(input[1], out int result))
                    {
                        if (result == 1)
                        {
                            dw.ShouldAutoSend = true;
                            Console.WriteLine("Enabled AutoSend");
                        }
                        else if (result == 0)
                        {
                            dw.ShouldAutoSend = false;
                            Console.WriteLine("Disabled AutoSend");
                        }
                        else
                        {
                            Console.WriteLine("Could\'t change AutoSend");
                        }
                    }
                }
                else if (input[0].ToLower() == "autosendtime")
                {
                    if (input.Length < 2) continue;
                    if (int.TryParse(input[1], out int result))
                    {
                        if (result > 0)
                        {
                            Console.WriteLine("Set AutoSendTime from {0} to {1}", dw.AutoSendCount, result);
                            dw.AutoSendCount = result;
                        }
                    }
                }
                else if (input[0].ToLower() == "setkey")
                {
                    if (input.Length < 2) continue;
                    if (dw.SetKey(input[1].ToLower()))
                    {
                        Console.WriteLine("Changed key : {0}", input[1].ToLower());
                    }
                    else
                    {
                        Console.WriteLine("Could\'t change key");
                    }
                }
                else if (input[0].ToLower() == "settext")
                {
                    if (input.Length < 2) continue;
                    if (input.Length == 2 && dw.SetText(input[1].ToLower()))
                    {
                        Console.WriteLine("Changed text to : {0}" , input[1].ToLower());
                    }
                    else if (input.Length > 2)
                    {
                        string[] vs = new string[input.Length-1];
                        for (int i = 1; i < input.Length; i++)
                        {
                            vs[i-1] = input[i].ToLower();
                        }
                        if (dw.SetTextWithSpaces(vs))
                        {
                            Console.WriteLine("Succesfully changed text");
                        }
                        else
                        {
                            Console.WriteLine("Could\'t change key");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Could\'t change key");
                    }
                }
                else if (input[0].ToLower() == "sleep")
                {
                    if (input.Length < 2) continue;
                    if (int.TryParse(input[1], out int st))
                    {
                        if (st > 0)
                        {
                            Console.WriteLine("Set sleeptime from {0} to {1}", dw.SleepTime, st);
                            dw.SleepTime = st;
                        }
                    }
                }
            }
        }
    }
}