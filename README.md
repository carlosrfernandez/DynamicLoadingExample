# DynamicLoading of assemblies that implement a common interface.
Just some fiddling around with reflection and dynamic assembly loading and some threading stuff.

This project uses a file system watcher to listen for new files. If a DLL implementing IHandleFile is dropped into the startup folder (supplied by
the command line arguments), it is loaded into the application, a check is done to ensure the implementation of IHandleFile, then
an instance is created and stored in an internal dictionary. 

TODO:

- Decouple all of the assembly loading stuff.
- ~Use messages~
- Use commands and use messages correctly ;)
- Use queues and threads to be able to load a massive amount of files to parse
- ~Load assemblies that have multiple implementations of the interface~