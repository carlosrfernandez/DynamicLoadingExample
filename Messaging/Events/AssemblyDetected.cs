using Core;

namespace Messaging.Events
{
    public class AssemblyDetected : IMessage
    {
        public string Path { get; set; }
    }
}