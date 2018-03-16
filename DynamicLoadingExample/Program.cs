using System;
using System.IO;
using FileWatcher;

namespace DynamicLoadingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var requestedFolder = args[0];
            if (!Directory.Exists(requestedFolder))
            {
                Console.WriteLine("Yep... no folder or not found. Will exit now");
                Console.ReadKey();
                return;
            }

            new FileWatcherManager(requestedFolder);
            Console.WriteLine("Listening for Dlls or files to parse...");

            while (true)
            {
                   
            }

        }
    }
}
