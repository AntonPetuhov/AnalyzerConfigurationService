using AnalyzerConfigurationService.Models;
using System.Text.Json;


namespace AnalyzerConfigurationService
{
    public class Worker : BackgroundService
    {
        private readonly AnalyzerManager analyzerManager;
        private AnalyzerSettings? analyzerSettings;

        private readonly List<Analyzer> analyzersStartList = new();

        public string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs"); 

        public Worker(AnalyzerManager analyzerManager)
        {
            this.analyzerManager = analyzerManager;
        }

        /// <summary>
        /// Запуск службы и начало работы
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 1. Первичная загрузка всех конфигураций
            LoadAllAnalyzers();

            // 2. Запускаем все анализаторы
            await StartAllAnalyzersAsync(stoppingToken);

            // 3. Настраиваем мониторинг папки
            StartWatching();

            try
            {
                // чтение настроек анализатора из json
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AnalyzerConfiguration.json");
                analyzerSettings = GetSettingsFromJson(configPath);

                // создаем объект анализатора и запускаем его
                Analyzer analyzerToRun = analyzerManager.CreateAnalyzer(analyzerSettings);
                await analyzerToRun.StartAsync();

                // Ожидаем сигнал остановки, не расходуя ресурсы
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log"), ex.ToString());
            }

        }

        /// <summary>
        /// Загрузка всех имеющихся конфигураций анализаторов (файлы с настройками json)
        /// </summary>
        private void LoadAllAnalyzers()
        {
            var files = Directory.GetFiles(configPath, "*.json");
            foreach (var file in files) 
            {
                Console.WriteLine(file);
                try
                {
                    analyzerSettings = GetSettingsFromJson(file);
                    // Проверяем, должен ли быть запущен анализатор
                    if (!analyzerSettings.activeStatus)
                    {
                        Console.WriteLine($"Анализатор {analyzerSettings.analyzerName} отключён (ActiveStatus = {analyzerSettings.activeStatus})");
                        continue;
                    }
                }
                catch (Exception ex) 
                {

                }
            }
        }

        /// <summary>
        /// Остановка службы
        /// </summary>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await analyzerManager.StopAllAsync();
            await base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// получение данных из JSON
        /// </summary>
        public AnalyzerSettings GetSettingsFromJson(string jsonPath)
        {
            if (string.IsNullOrWhiteSpace(jsonPath))
                throw new ArgumentException("Путь не может быть пустым.", nameof(jsonPath));

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Файл конфигурации не найден: {jsonPath}");

            try
            {
                // Настройка десериализации: не учитывать регистр имён свойств
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                string jsonString = File.ReadAllText(jsonPath);

                // Десериализация JSON в объект AnalyzerSettings
                // ?? null?объединяющий оператор (null?coalescing operator)
                // возвращает результат своего левого операнда, если он существует и не равен null, а в противном случае возвращает правый операнд
                AnalyzerSettings settings = JsonSerializer.Deserialize<AnalyzerSettings>(jsonString, options) ?? throw new InvalidOperationException("Не удалось десериализовать настройки");

                if (settings is null)
                    throw new InvalidOperationException("Десериализация вернула null. Проверьте содержимое JSON.");

                return settings;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
