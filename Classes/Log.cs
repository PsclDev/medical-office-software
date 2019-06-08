using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MoS.Classes {
    public static class Log {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMethodName() {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            var reflectedtype = sf.GetMethod().ReflectedType.ToString();
            string[] classname = reflectedtype.Split('.');
            string info = $"{classname[1]}.{sf.GetMethod().Name}";

            return info;
        }

        private static string exePath = string.Empty;

        public static string GetPath(string filename = "") {
            exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string LogDir = Path.Combine(exePath, "Log");
            Directory.CreateDirectory(LogDir);

            if (filename != "") {
                string LogFile = Path.Combine(LogDir, filename);

                if (!File.Exists(LogFile)) {
                    var fs = File.Create(LogFile);
                    fs.Close();
                }

                return LogFile;
            }
            else
                return LogDir;
        }

        public static List<string> GetLogFiles() {
            List<string> Files = new List<string>
            {
                GetPath("Log.Err"),
                GetPath("Log.txt")
            };
            return Files;
        }

        public static void Trace(string Function = "", string Message = "") {
            try {
                string filename = "log.txt";
                string path = GetPath(filename);
                string timestamp = DateTime.UtcNow.ToString("dd:MM:yyyy HH:mm:ss");
                string log;

                if (Function != "")
                    log = $"{Function}     -     {Message}";
                else
                    log = $"{Message}";

                using (StreamWriter sw = File.AppendText(path))
                    Write(timestamp, log, sw);
            }
            catch (Exception exc) {
                Log.Error($"Log.Trace({Function}, {Message})", exc.ToString());
            }
        }

        public static void Error(string Function = "", string Message = "") {
            try {
                string filename = "log.err";
                string path = GetPath(filename);
                string timestamp = DateTime.UtcNow.ToString("dd:MM:yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);

                string log = $"{Function}     -     {Message}\n---------------------------------------------------------------------------------------";
                using (StreamWriter sw = File.AppendText(path))
                    Write(timestamp, log, sw);
            }
            catch {
                CstmMsgBx.Error("Couldn´t write to log file");
                System.Threading.Thread.Sleep(5000);
                Application.Current.Shutdown();
            }
        }

        private static void Write(string Timestamp, string Message, TextWriter Log) {
            Log.WriteLine($"{Timestamp}     -     {Message}");
        }
    }
}
