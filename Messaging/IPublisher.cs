using Core;

namespace Messaging
{
    public interface IPublisher
    {
        void Publish<T>(T message) where T : IMessage;
    }
}