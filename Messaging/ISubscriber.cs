using System;
using Core;

namespace Messaging
{
    public interface ISubscriber
    {
        void Subscribe<T>(IMessageHandler<T> messageHandler) where T : IMessage;
        void Subscribe<T>(Type topic, IMessageHandler<T> messageHandler) where T : IMessage;
        void Subscribe<T>(string topic, IMessageHandler<T> messageHandler) where T : IMessage;
    }
}