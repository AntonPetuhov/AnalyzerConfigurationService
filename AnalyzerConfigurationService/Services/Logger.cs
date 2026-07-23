using System;
using System.Collections.Generic;
using System.Text;
using AnalyzerConfigurationService.Interfaces;

namespace AnalyzerConfigurationService.Services
{
    /*
    public enum LoggerType
    {
        Service,        // Общий лог службы
        TcpIp,          // Лог TCP/IP соединения
        Exchange,       // Лог обмена ASTM-сообщениями (запросы/ответы)
        Result,         // Лог обработчика файлов результатов
        Request         // Лог обработки сообщения с запросом задания и сопоставления с профилями
    }
    */

    public class Logger : ILoggerService 
    {
        public LoggerType loggerType { get; set; }      // тип логгера
        public string logsDirectory { get; set; }       // директория логов

        private readonly object locker = new object();

        public Logger(string analyzerPath)
        {
            logsDirectory = Path.Combine(analyzerPath, "Logs");
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }
        }

        public void LogService(string message) => Write(LoggerType.Service, message);
        public void LogTcp(string message) => Write(LoggerType.TcpIp, message);
        public void LogExchange(string message) => Write(LoggerType.Exchange, message);
        public void LogResult(string message) => Write(LoggerType.Result, message);
        public void LogRequest(string message) => Write(LoggerType.Request, message);

        private void Write(LoggerType loggerType, string message)
        {
            lock (locker)
            {
                try
                {
                    string currentLogPath = Path.Combine(logsDirectory, loggerType.ToString());
                    if (!Directory.Exists(currentLogPath))
                    {
                        Directory.CreateDirectory(currentLogPath);
                    }

                    string logfileName = currentLogPath + $"\\{loggerType}Log_" + DateTime.Now.ToShortDateString().Replace('/', '_') + ".txt";

                    if (!File.Exists(logfileName))
                    {
                        using (StreamWriter writer = File.CreateText(logfileName))
                        {
                            writer.WriteLine(DateTime.Now + ": " + message);
                        }
                    }
                    else
                    {
                        using (StreamWriter writer = File.AppendText(logfileName))
                        {
                            writer.WriteLine(DateTime.Now + ": " + message);
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }

}
