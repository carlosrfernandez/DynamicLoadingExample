namespace Contract
{
    public interface IHandleFile
    {
        bool Parse(string filePath);
        
        // this could be a class with rules
        string Extension { get; }
    }
}