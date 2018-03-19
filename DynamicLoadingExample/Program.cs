using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core;
using DllHandler;
using FileProcess;
using FileWatcher;
using Messaging;
using Messaging.Events;

namespace DynamicLoadingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Sanitize(args, out var requestedFolder)) return;

            var bus = new InMemoryPubSub();
            var fileHandlers = new ConcurrentDictionary<string, List<Type>>();
            var assemblyValidator = new AssemblyValidator(bus);
            var dllLoader = new NewFileTypeSubscriber(fileHandlers);

            var fileManager = new Manager(fileHandlers);

            bus.Subscribe(assemblyValidator);
            bus.Subscribe(dllLoader);
            bus.Subscribe(fileManager);
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
