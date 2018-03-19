using System.IO;
using System.Threading;
using Contract;

namespace CsvFileParser
{
    public class CsvFileParser : IHandleFile
    { 
        public bool Parse(string filePath)
        {

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            // don't care about parsing ATM
            Thread.Sleep(500);
            return true;
        }

        public string Extension { get; } = ".csv";
    }
}