using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Contract;

namespace DllHandler
{
    public class AssemblyHandler : IAssemblyHandler
    {
        public bool Parse(string filePath)
        {
            if (Path.GetExtension(filePath) != Extension)
            {
                return false;
            }
            // dynamic loading
            var assembly = Assembly.LoadFile(filePath);
            var definedTypes = assembly.DefinedTypes;
            if (!definedTypes.Any(x => typeof(IHandleFile).IsAssignableFrom(x))) return false;
            {
                // get fileType 
                var type = assembly.GetTypes().FirstOrDefault(x => typeof(IHandleFile).IsAssignableFrom(x));
                if (type == null) return false;

                //get all files this assembly can handle;
                var typeInstance = (IHandleFile) Activator.CreateInstance(type);

                var fileExtensionProperty = (string) type.GetProperty(nameof(IHandleFile.Extension))?.GetValue(typeInstance);
                Console.WriteLine($"Found new handler for extension {fileExtensionProperty}");

                HandlerInstance = typeInstance;
;
                return true;
            }
        }

        public string Extension { get; } = ".dll";
        public IHandleFile HandlerInstance { get; private set; }
    }

    public interface IAssemblyHandler : IHandleFile
    {
        IHandleFile HandlerInstance { get; }
    }
}
