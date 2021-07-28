using RightPlaceBL.Server.Model;
using RightPlaceBL.DataBase;
using RightPlaceBL;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RightPlaceServerConsole
{
    class Program
    {
        static Server server; // сервер
        static Task listenTask; // потока для прослушивания
        static void Main(string[] args)
        {
            
            try
            {
                server = new Server();
                listenTask = new Task(server.Listen);
                listenTask.Start(); //старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
