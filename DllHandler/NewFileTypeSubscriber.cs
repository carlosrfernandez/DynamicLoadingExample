using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Contract;
using Core;
using Messaging;
using Messaging.Events;

namespace DllHandler
{
    public class NewFileTypeSubscriber : IMessageHandler<FileHandlerLoadedFromAssembly>
    {
        private readonly ConcurrentDictionary<string, List<Type>> _fileHandlers;

        public NewFileTypeSubscriber(ConcurrentDictionary<string, List<Type>> fileHandlers)
        {
            _fileHandlers = fileHandlers;
        }

        public void Handle(FileHandlerLoadedFromAssembly message)
        {
            if (_fileHandlers.TryGetValue(message.Extension, out var currentHandlers))
            {
                var newHandlers = new List<Type>(currentHandlers) { message.FullyQualifiedType };

                _fileHandlers[message.Extension] = newHandlers;
            }
            else
            {
                _fileHandlers[message.Extension] = new List<Type> { message.FullyQualifiedType };
            }
        }
    }
}