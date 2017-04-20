using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Interface : IClientHandler. The Interface has "HandleClient" function.
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        void HandleClient(TcpClient client);
    }
}