using AnalyzerConfigurationService.Models;
using AnalyzerConfigurationService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerConfigurationService
{
    /// <summary>
    /// Менеджер анализаторов, фабрика анализаторов. Фабричный подход
    /// </summary>
    public class AnalyzerManager
    {
        private readonly IAnalyzerLoggerFactory loggerFactory; // DI
        private readonly List<Analyzer> analyzersList = new();

        public AnalyzerManager(IAnalyzerLoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Cоздание объекта анализатора
        /// </summary>
        public Analyzer CreateAnalyzer(AnalyzerSettings analyzersettings)
        {
            var analyzer = new Analyzer(loggerFactory, analyzersettings);
            analyzersList.Add(analyzer);
            return analyzer;
        }

        // потребуется исключить попытку повторного запуска анализатора, для проекта по анализаторам, метод GetStatus?
        /// <summary>
        /// Запуск всех анализаторов
        /// </summary>
        public async Task StartAllAsync()
        {
            foreach (var analyzer in analyzersList)
            {
                Console.WriteLine($"Запуск прибора: {analyzer}");
                // добавить проверку activeStatus
                await analyzer.StartAsync();
            }
        }

        /// <summary>
        /// Остановка всех анализаторов
        /// </summary>
        public async Task StopAllAsync()
        {
            foreach (var analyzer in analyzersList)
            {
                try
                {
                    await analyzer.StopAsync();
                }
                catch (Exception) { }
                finally
                {
                    analyzer.Dispose(); // переиспользовать объект анализатора будет нельзя, только создать новый, тк освобождаем ресурсы
                }
            }
        }


    }
}
