using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Contract;
using Core;
using Messaging;
using Messaging.Messages;

namespace DllHandler
{
    // This guy is a bit slower because reflection.
    public class AssemblyValidator : IMessageHandler<AssemblyDetected>, IHandleFile
    {
        private readonly IPublisher _publisher;
        public string Extension { get; } = ".dll";
        
        public AssemblyValidator(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(AssemblyDetected message)
        {
            Parse(message.Path);
        }

        public bool Parse(string filePath)
        {
            if (Path.GetExtension(filePath) != Extension)
            {
                return false;
            }

            var assembly = Assembly.LoadFile(filePath);
            var definedTypes = assembly.DefinedTypes.ToList();
            if (!definedTypes.Any(x => typeof(IHandleFile).IsAssignableFrom(x))) return false;
            {
                foreach (var type in definedTypes.Where(x => typeof(IHandleFile).IsAssignableFrom(x)))
                {
                    //get all files this assembly can handle;
                    try
                    {
                        var typeInstance = (IHandleFile) Activator.CreateInstance(type); // todo careful here this might fail.
                        var fileExtensionProperty = (string) type.GetProperty(nameof(IHandleFile.Extension))?.GetValue(typeInstance);
                        if (string.IsNullOrWhiteSpace(fileExtensionProperty)) continue;
                        _publisher.Publish(new FileHandlerLoadedFromAssembly
                        {
                            AssemblyPath = filePath,
                            Extension = fileExtensionProperty,
                            FullyQualifiedType = type
                        });
                    }
                    catch
                    {
                        // add logging 
                    }
                }
            }
            
            return true;
        }
    }
}
