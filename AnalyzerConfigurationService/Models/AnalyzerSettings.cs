using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerConfigurationService.Models
{
    /// <summary>
    /// Класс настроек, требующихся для работы анализатора
    /// </summary>
    public class AnalyzerSettings
    {
        // required - обязательные поля
        // ? - поля, которые могут отсутствовать
        public string? analyzerId { get; set; }                 // Уникальный ID прибора
        public required string analyzerName { get; set; }       // Уникальный ID прибора
        public required string connectionType { get; set; }     // "TCPIP", "Serial", "File"
        public string? ipAddress { get; set; }                  // IP-адрес, на котором слушаем
        public int port { get; set; }                           // Порт для TCP/IP
        public required bool isdll { get; set; }                // подключен с помощью dll?
        public string? dllPath { get; set; }                    // Путь к dll с реализацией протокола

        // Статусы активности (управление потоками)
        public bool activeStatus { get; set; }           // статус работы прибора
        public bool workStatus { get; set; }             // запускать ли поток обмена сообщениями с прибором
        public bool resultHandlerStatus { get; set; }    // запускать ли поток обработки результатов

        // Папки для сохранения результатов и логов
        public required string resultsFolder { get; set; }  // Корневая папка для результатов
        public string? logsFolder { get; set; }             // Папка для логов

        // папка службы FileGetterService, которая обрабатывает и записывает результаты в CGM
        public string? outputFolder { get; set; }

        // Строка подключения к БД
        public required string connectionString { get; set; }

        // коды анализатора в CGM Analyzer Congiguration 
        public string? analyzerCode { get; set; }
        public string? analyzerConfigurationCode { get; set; }
    }
}
