using System;

namespace TBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot tBot = new Bot();
            tBot.Connect(true);
            Console.ReadLine();
            tBot.Disconnect();

        }
    }
}
