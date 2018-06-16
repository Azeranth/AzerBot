using System;

namespace AzerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.Connect();
            do
            {
                Console.ReadLine();
            } while (true);
        }
    }
}
