namespace Contract
{
    public interface IParseFile
    {
        bool Parse(string filePath);
        
        // this could be a class with rules
        string Extension { get; }
    }
}