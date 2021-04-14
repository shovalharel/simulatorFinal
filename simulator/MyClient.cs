using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace simulator
{
    class MyClient : IClient
    {
        Socket client;
        public LinkedList<String> csv_file;

        // constructor
        public MyClient()
        {
            // Create a TCP socket.
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            csv_file = new LinkedList<string>();
        }

        void IClient.connect(IPEndPoint ipEndPoint, string file_path)
        { 

            Console.WriteLine("after socket");
            // Connect the socket to the remote endpoint.
            client.Connect(ipEndPoint);
            //string line;
            //StreamReader sr = new StreamReader(file_path);

            //save all lines in csv file at list
            /*while ((line = sr.ReadLine()) != null)
            {
                csv_file.AddLast(line);

            }*/
            Console.WriteLine("after connect");
        }

        void IClient.disconnect()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        string IClient.read(int index)
        {
            return csv_file.ElementAt(index);
        }

        void IClient.write(string line)
        {
            client.Send(Encoding.ASCII.GetBytes(line));
            client.Send(Encoding.ASCII.GetBytes("\r\n"));
        }
    }
}
