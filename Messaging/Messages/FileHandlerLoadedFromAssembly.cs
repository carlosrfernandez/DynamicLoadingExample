using System;
using Core;

namespace Messaging.Messages
{
    public class FileHandlerLoadedFromAssembly : IMessage
    {
        public string AssemblyPath { get; set; }
        public string Extension { get; set; }
        // sending type information in a message based application is probably not
        // a great idea. this is just for my fiddling purposes.
        public Type FullyQualifiedType { get; set; }
    }
}