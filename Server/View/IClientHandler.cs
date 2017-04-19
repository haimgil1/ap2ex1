using System.Net.Sockets;

namespace Server
{
	/// <summary>
	/// Client handler.
	/// </summary>
	public interface IClientHandler
	{
		void HandleClient(TcpClient client);
	}
}