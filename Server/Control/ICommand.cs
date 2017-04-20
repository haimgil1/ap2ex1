using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Interface : ICommand. The Interface has "Execute" function.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string: "singlePlayer" or "multiPlayer"</returns>
        string Execute(string[] args, TcpClient client = null);
    }
}
