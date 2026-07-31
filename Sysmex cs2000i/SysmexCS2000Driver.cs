using AnalyzerConfigurationService.Interfaces;
using AnalyzerConfigurationService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sysmex_cs2000i
{
    public class SysmexCS2000Driver : IAnalyzerDriver
    {
        private ILoggerService logger;
        private AnalyzerSettings? settings;
        private CancellationTokenSource? cts;

        private ITcpHost tcpHost = null!;
    }
}
