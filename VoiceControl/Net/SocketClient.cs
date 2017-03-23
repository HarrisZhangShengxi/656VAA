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

            IPAddress ipAddress = IPAddress.Parse(HostIP);  //Transform the string into IPAddress

            //Initialize the socket for client using TCP
            clientSocket = new Socket(  
              AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);

            do
            {
                try
                {
                    clientSocket.Connect(ipAddress, HostPort);  //Connect to Server
                    a = "Client is connected.\n";
                    break;
                }
                catch (Exception e)
                {
                    time++;
                    if (time < RETRY_TIME)  //If timeout, try to reconncet
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    else a = "Time out.\n";
                }
            } while (time < RETRY_TIME);
            return a;
        }

        /*public static void reconnect(string HostIP, int HostPort)
        {
            if(clientSocket != null)
            {
                if(clientSocket.Connected == false)
                {
                    IPAddress ipAddress = IPAddress.Parse(HostIP);
                    clientSocket.Connect(ipAddress, HostPort);
                }
            }
        }*/

        public static string send(string input) //send data to server
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

        /*public static string receive()  //receive data from server
        {
            string b = "";
            byte[] receiveBuffer = null;

            if (clientSocket.Connected == true)
            {
                clientSocket.Receive(receiveBuffer);
                char[] a = Encoding.ASCII.GetChars(receiveBuffer);
                b = a.ToString();
            }
            return b;
        }*/

        public static string disconnect()   //disconnection
        {
            string a = "";

            if (clientSocket.Connected == true)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                a = "Disconnected.";
            }
            else a = "No connection existed.";
            return a;
        }
    }
}