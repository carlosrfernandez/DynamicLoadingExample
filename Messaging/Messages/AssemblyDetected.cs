using Core;

namespace Messaging.Messages
{
    public class AssemblyDetected : IMessage
    {
        public string Path { get; set; }
    }
}