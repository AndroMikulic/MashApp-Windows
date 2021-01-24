using System;
using System.IO;

namespace MashApp
{
    public static class Logger
    {
        static String logFilePath = "log.txt";
        public static void Log(String entry)
        {
            if (!File.Exists(logFilePath))
            {
                // Create a file to write to.
                using (StreamWriter log = File.CreateText(logFilePath))
                {
                    log.WriteLine(DateTime.Now + " - Log created");
                }
            }

            try
            {
                using (StreamWriter log = File.AppendText(logFilePath))
                {
                    log.WriteLine(DateTime.Now + " - " + entry);
                }
            }
            catch
            {
                Log(entry);
            }
        }
    }
}
