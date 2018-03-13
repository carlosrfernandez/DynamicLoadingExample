using System;
using System.IO;

namespace FileWatcher
{
    public class FileWatcherManager : IDisposable
    {
        private readonly FileSystemWatcher _fileSystemWatcher;

        public FileWatcherManager(string path)
        {
            if (!Directory.Exists(path)) throw new ArgumentException();

            _fileSystemWatcher = new FileSystemWatcher(path);
            _fileSystemWatcher.Created += FileSystemWatcherOnCreated;
        }

        private void FileSystemWatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            _fileSystemWatcher?.Dispose();
        }
    }
}