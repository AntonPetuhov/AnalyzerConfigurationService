using System;
using System.Collections.Generic;
using System.Text;
using AnalyzerConfigurationService.Services;

namespace AnalyzerConfigurationService.Interfaces
{
    public enum LoggerType
    {
        Service,        // Общий лог службы
        TcpIp,          // Лог TCP/IP соединения
        Exchange,       // Лог обмена ASTM-сообщениями (запросы/ответы)
        Result,         // Лог обработчика файлов результатов
        Request         // Лог обработки сообщения с запросом задания и сопоставления с профилями
    }
    public interface ILoggerService
    {
        LoggerType loggerType { get; set; } // тип логгера
        string logsDirectory { get; set; }  // папка для записи логов

        void LogService(string message);
        void LogTcp(string message);
        void LogExchange(string message);
        void LogResult(string message);
        void LogRequest(string message);
    }
}
