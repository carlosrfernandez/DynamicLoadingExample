using Core;

namespace Messaging.Messages
{
    public class ContentFileCreated : IMessage
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
    }
}