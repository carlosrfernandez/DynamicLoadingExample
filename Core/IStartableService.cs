namespace Core
{
    public interface IStartableService
    {
        void Start();
        string Name { get; }
    }
}