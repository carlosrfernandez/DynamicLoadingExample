# DynamicLoadingExample
Just some fiddling around with reflection and dynamic assembly loading

This project uses a file system watcher. 

If a DLL implements IHandleFile is dropped into the startup folder (supplied by argument), it 
is loaded into the context, a check is done to ensure the implementation of IHandleFile, then
an instance is created. 

TODO:

Decouple all of the assembly loading stuff.
Use messages
Use queues and threads to be able to load a massive amount of files to parse
Load assemblies that have multiple implementations of the interface

