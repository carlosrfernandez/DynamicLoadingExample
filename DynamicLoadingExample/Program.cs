using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core;
using DllHandler;
using FileWatcher;
using Messaging;
using Messaging.Messages;

namespace DynamicLoadingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new InMemoryPubSub();

            if (Sanitize(args, out var requestedFolder)) return;
            
            var dllHandler = new AssemblyValidator(bus);
            var dllLoader = new FileHandlerCoordinator();
            bus.Subscribe(dllHandler);
            bus.Subscribe<FileHandlerLoadedFromAssembly>(dllLoader);
            bus.Subscribe<ContentFileCreated>(dllLoader);
            var fileSystemWatcher = new FileWatcherManager(requestedFolder, bus);
            var startables = new List<IStartableService> { fileSystemWatcher };
            startables.ForEach(x => x.Start());

            Task.Run(function: () =>
            {
                Console.WriteLine("Listening for Dlls or files to parse...");
                while (true) { }
            }).Wait();
        }

        private static bool Sanitize(IReadOnlyList<string> args, out string requestedFolder)
        {
            requestedFolder = args[0];
            if (Directory.Exists(requestedFolder)) return false;

            Console.WriteLine("Yep... no folder or not found. Will exit now");
            Console.ReadKey();
            return true;

        }
    }
}
