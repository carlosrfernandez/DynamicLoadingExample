using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Contract;
using Core;
using Messaging.Messages;

namespace DllHandler
{
    // this could be a process...
    public class FileHandlerCoordinator : IMessageHandler<FileHandlerLoadedFromAssembly>, IMessageHandler<ContentFileCreated>
    {
        private readonly ConcurrentDictionary<string, List<IHandleFile>> _fileHandlers = new ConcurrentDictionary<string, List<IHandleFile>>();

        public void Handle(FileHandlerLoadedFromAssembly message)
        {
            // this could be done in the actual handling of a file, we'll see.
            var extension = message.Extension;

            var handler = (IHandleFile)Activator.CreateInstance(message.FullyQualifiedType);

            if (_fileHandlers.TryGetValue(extension, out var currentHandlers))
            {
                var newHandlers = new List<IHandleFile>(currentHandlers) { handler };

                _fileHandlers[extension] = newHandlers;
            }
            else
            {
                _fileHandlers[extension] = new List<IHandleFile> { handler };
            }
        }

        public void Handle(ContentFileCreated message)
        {
            if (!_fileHandlers.TryGetValue(message.FileExtension, out var handlers)) return;
            {
                var results = handlers.Select(h => new
                {
                    Result = h.Parse(message.FilePath),
                    FailedHandler = h.GetType().FullName
                });

                results.Where(x => !x.Result).ToList().ForEach(x => Console.WriteLine($"Error parsing {message.FilePath} with handler {x.FailedHandler}"));
            }
        }

        /*public void Start()
        {
            
        }

        public string Name { get; } = "ContentFileHandler";*/
    }
}