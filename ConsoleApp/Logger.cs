using System;
using System.IO;

namespace ECommerce.Logging
{
    public static class Logger
    {
        private static readonly string logFilePath = "logs.txt";

        static Logger()
        {
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
        }

        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            Log("ERROR", message);
        }

        private static void Log(string logType, string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logType}] {message}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
