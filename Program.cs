using System;

namespace GraphCandleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            startup.Start();

            Console.ReadKey();
        }
    }
}
