using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string port = ConfigurationManager.AppSettings["Port"].ToString();
            int portInt = Int32.Parse(port);
            ClientConnect client = new ClientConnect();
            // connect the client
            client.Connect(portInt);
        }
    }
}
