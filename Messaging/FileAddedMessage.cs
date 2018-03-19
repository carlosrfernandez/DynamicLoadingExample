namespace Messaging
{
    public class FileAddedMessage : IMessage
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
    }
}