

using System.Net;

namespace simulator
{
    internal interface IClient
    {
        void connect(IPEndPoint ipEndPoint, string file_path);
        void write(string line);
        string read(int index);
        void disconnect();

    }
}