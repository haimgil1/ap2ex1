using System.Net.Sockets;

namespace Server
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
    }
}