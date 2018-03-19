using Core;

namespace Messaging.Events
{
    public class ContentFileCreated : IMessage
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
    }
}