using AnalyzerConfigurationService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzerConfigurationService.Interfaces
{
    public interface IAnalyzer
    {
        AnalyzerSettings analyzerSettings { get; set; }
        Task StartAsync();
        Task StopAsync();

    }
}
