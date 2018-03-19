using System;
using Core;

namespace Messaging.Messages
{
    public class FileHandlerLoadedFromAssembly : IMessage
    {
        public string AssemblyPath { get; set; }
        public string Extension { get; set; }
        public Type FullyQualifiedType { get; set; }
    }
}