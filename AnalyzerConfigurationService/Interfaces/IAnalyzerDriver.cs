using AnalyzerConfigurationService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerConfigurationService.Interfaces
{
    public interface IAnalyzerDriver : IDisposable
    {
        // Инициализация драйвера (передаём логгер и настройки)
        void Initialize(ILoggerService logger, AnalyzerSettings settings);

        // Запуск работы анализатора
        Task StartCommunicationAsync(CancellationToken cancellationToken);

        // Остановка работы анализатора
        //Task StopCommunicationAsync(CancellationToken cancellationToken);
        Task StopCommunicationAsync();

        // Обработка результатов
        //Task ResultsHandlerAsync(CancellationToken cancellationToken);

        // Освобождение ресурсов
        //void Dispose(); // итак наследуем от IDisposable

    }
}
