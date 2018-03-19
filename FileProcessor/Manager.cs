using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Core;
using Messaging.Events;

namespace FileProcess
{
    public class Manager : IMessageHandler<ContentFileCreated>
    {
        private readonly ConcurrentDictionary<string, List<Type>> _typesDictionary;

        public Manager(ConcurrentDictionary<string, List<Type>> typesDictionary)
        {
            _typesDictionary = typesDictionary;
        }

        public void Handle(ContentFileCreated message)
        {
            if (!_typesDictionary.ContainsKey(message.FileExtension)) return;
            else
            {
                // do threading
            }
        }
    }
}
