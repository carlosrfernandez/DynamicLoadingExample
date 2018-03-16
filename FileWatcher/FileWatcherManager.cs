using System;
using System.Collections.Generic;
using System.IO;
using Contract;
using DllHandler;

namespace FileWatcher
{
    public class FileWatcherManager : IDisposable
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly Dictionary<string, IHandleFile> _handlers;
        private readonly IAssemblyHandler _assemblyHandler = new AssemblyHandler();

        public FileWatcherManager(string path)
        {
            if (!Directory.Exists(path)) throw new ArgumentException();
            _handlers = new Dictionary<string, IHandleFile>();

            _fileSystemWatcher = new FileSystemWatcher(path) { EnableRaisingEvents = true };
            _fileSystemWatcher.Created += FileSystemWatcherOnCreated;
        }

        // only handle created for now
        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            var fileExtension = Path.GetExtension(fileSystemEventArgs.FullPath);
            if (fileExtension == null) return;

            if (fileExtension == _assemblyHandler.Extension)
            {
                if (_assemblyHandler.Parse(fileSystemEventArgs.FullPath))
                {
                    _handlers.Add(_assemblyHandler.HandlerInstance.Extension, _assemblyHandler.HandlerInstance);
                }
                else
                {
                    Console.WriteLine("Error handling assembly.");
                }

                return;
            } 

            if (!_handlers.ContainsKey(fileExtension))
            {
                Console.WriteLine($"No handler for file extension: {fileExtension}");
                return;
            }

            // handle.
            var handleResult = _handlers[fileExtension].Parse(fileSystemEventArgs.FullPath);
            if (!handleResult)
            {
                // log.
                Console.WriteLine($"Could not properly parse {fileSystemEventArgs.FullPath}");
            }
        }


        public void Dispose()
        {
            _fileSystemWatcher?.Dispose();
        }
    }

    /*public interface IMessage
    {

    }

    public class FileAddedMessage : IMessage
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
    }


    public interface IPublisher
    {
        void Publish(IMessage message);
    }

    public interface ISubscriber
    {
        void Subscribe(IMessage message);
    }

    public class Publisher : IPublisher, ISubscriber
    {
        private readonly ConcurrentDictionary<string, IHandleFile> _handlers = new ConcurrentDictionary<string, IHandleFile>();

        public void Publish(IMessage message)
        {
            
        }

        public void Subscribe(IMessage message)
        {
            throw new NotImplementedException();
        }
    }*/
}