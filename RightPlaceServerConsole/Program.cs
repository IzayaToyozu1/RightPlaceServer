using RightPlaceBL.Server.Model;
using RightPlaceBL.DataBase;
using RightPlaceBL;
using System;
using System.Threading;

namespace RightPlaceServerConsole
{
    class Program
    {
        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания
        static ApplicationContext database = new ApplicationContext();
        static void Main(string[] args)
        {
            
            try
            {
                server = new ServerObject();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
