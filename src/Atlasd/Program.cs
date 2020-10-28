﻿using Atlasd.Daemon;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Atlasd
{
    class Program
    {
        // used for printing debug or release in version string
        private const string DistributionMode =
#if DEBUG
            "debug";
#else
            "release";
#endif

        // used to signal process exit from other locations in code
        public static bool Exit = false;
        public static int ExitCode = 0;

        public static async Task<int> Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";

            var assembly = typeof(Program).Assembly;
            Console.WriteLine($"[{DateTime.Now}] Welcome to {assembly.GetName().Name}!");
            Console.WriteLine($"[{DateTime.Now}] Build: {assembly.GetName().Version} ({DistributionMode})");

#if DEBUG
            // wait for debugger to attach to process
            if (!Debugger.IsAttached)
            {
                Console.WriteLine($"[{DateTime.Now}] Waiting for debugger to attach...");
                while (!Debugger.IsAttached)
                {
                    await Task.Yield();
                }
                Console.WriteLine($"[{DateTime.Now}] Debugger attached: {Debugger.IsAttached}");
            }
#endif

            ParseCommandLineArgs(args);
            Settings.Initialize();
            Battlenet.Common.Initialize();
            Battlenet.Common.Listener.Start();

            while (!Exit)
            {
                await Task.Delay(10);
            }

            return ExitCode;
        }
        private static void ParseCommandLineArgs(string[] args)
        {
            string arg;
            string value;

            for (var i = 0; i < args.Length; i++)
            {
                arg = args[i];

                if (arg.Contains('='))
                {
                    var p = arg.IndexOf('=');
                    value = arg.Substring(p + 1);
                    arg = arg.Substring(0, p);
                }
                else if (i + 1 < args.Length)
                {
                    value = args[++i];
                }
                else
                {
                    value = "";
                }

                var r = ParseCommandLineArg(arg, value);
                if (r != 0)
                {
                    Program.ExitCode = r;
                    Program.Exit = true;
                    return;
                }
            }
        }

        /**
         * <returns>Program exit code, return zero (0; success) to continue to next argument.</returns>
         */
        private static int ParseCommandLineArg(string arg, string value)
        {
            const int EXIT_SUCCESS = 0;
            const int EXIT_FAILURE = 1;

            switch (arg)
            {
                case "-c":
                case "--config":
                    {
                        Daemon.Settings.SetPath(value);
                        break;
                    }
                default:
                    {
                        Logging.WriteLine(Logging.LogLevel.Error, Logging.LogType.Config, $"Invalid argument [{arg}]");
                        return EXIT_FAILURE;
                    }
            }

            return EXIT_SUCCESS;
        }
    }
}
 