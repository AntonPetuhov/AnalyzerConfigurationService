using AnalyzerConfigurationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerConfigurationService.Services
{
    // для создания логгера, фабричный подход
    public interface IAnalyzerLoggerFactory
    {
        ILoggerService CreateLogger(string folderPath);
    }

    public class AnalyzerLoggerFactory : IAnalyzerLoggerFactory
    {
        public ILoggerService CreateLogger(string folderPath) => new Logger(folderPath);
    }
}
