using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AnalyzerConfigurationService.Interfaces
{
    // TCP-хост, управляющий подключениями анализатора
    public interface ITcpHost : IDisposable
    {
        void Start(IPAddress ipAddress, int port);
        Task<TcpClient> AcceptClientAsync(CancellationToken cancellationToken);
        void Stop();
    }
}
