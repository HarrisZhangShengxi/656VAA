using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace VoiceControl.Net
{
    class SocketClient
    {
        
        public static Socket clientSocket;

        public static string SocketConnect(string HostIP, int HostPort)
        {
            int time = 0;
            int RETRY_TIME = 3;

            string a = "";

            IPAddress ipAddress = IPAddress.Parse(HostIP);

            clientSocket = new Socket(
              AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);

            do
            {
                try
                {
                    clientSocket.Connect(ipAddress, HostPort);
                    a = "Client is connected.\n";
                    break;
                }
                catch (Exception e)
                {
                    time++;
                    if (time < RETRY_TIME)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                }
            } while (time < RETRY_TIME);
            return a;
        }

        public static string send(string input)
        {
            string a = "";

            if (clientSocket.Connected == true)
            {
                byte[] sendBuffer = Encoding.ASCII.GetBytes(input);
                clientSocket.Send(sendBuffer);
                a = "Sent data.\n";
            }
            else a = "Operation failed.\n";
            return a;
        }

        public static string disconnect()
        {
            string a = "";

            if(clientSocket.Connected == true)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                a = "Disconnected.\n";
            }
            a = "No connection existed.\n";
            return a;
        }
    }
}