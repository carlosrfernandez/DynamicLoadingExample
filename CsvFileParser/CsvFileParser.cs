using Contract;
using System.IO;

namespace CsvFileParser
{
    public class CsvFileParser : IParseFile
    {
        public bool Parse(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            // don't care about parsing ATM
            return Path.GetExtension(filePath) == Extension;
        }

        public string Extension { get; } = "csv";
    }
}