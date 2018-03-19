using System;
using System.IO;
using System.Threading.Tasks;
using Core;
using Messaging;
using Messaging.Messages;

namespace FileWatcher
{
    public class FileWatcherManager : IDisposable, IStartableService
    {
        private readonly IPublisher _publisher;
        private readonly FileSystemWatcher _fileSystemWatcher;

        public FileWatcherManager(string path, IPublisher publisher)
        {
            if (!Directory.Exists(path)) throw new ArgumentException($"Directory {path} does not exist!");
            _publisher = publisher;
            _fileSystemWatcher = new FileSystemWatcher(path);
        }

        // only handle created for now
        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            var fileExtension = Path.GetExtension(fileSystemEventArgs.FullPath);

            if (fileExtension == ".dll")
            {
                _publisher.Publish(new AssemblyDetected { Path = fileSystemEventArgs.FullPath });
            }
            else
            {
                _publisher.Publish(new ContentFileCreated { FilePath = fileSystemEventArgs.FullPath, FileExtension = fileExtension });
            }
        }

        public void Start()
        {
            Task.Run(() =>
            {
                _fileSystemWatcher.Created += FileSystemWatcherOnCreated;
                _fileSystemWatcher.EnableRaisingEvents = true;

                while (true)
                {

                }
            });
        }

        public string Name { get; } = "FileSystemWatcher";


        public void Dispose()
        {
            _fileSystemWatcher?.Dispose();
        }
    }
}