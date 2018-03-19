using Core;
namespace Contract
{
    public interface IHandleFile : IMessage
    {
        bool Parse(string filePath);
        
        // this could be a class with rules
        string Extension { get; }
    }
}