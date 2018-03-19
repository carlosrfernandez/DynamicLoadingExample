using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Core;

namespace Messaging
{
    public class InMemoryPubSub : IPublisher, ISubscriber
    {
        private readonly ConcurrentDictionary<string, List<Action<IMessage>>> _subscribers = new ConcurrentDictionary<string, List<Action<IMessage>>>();

        public void Publish<T>(T message) where T : IMessage
        {
            var typeOfT = typeof(T);
            Publish(typeOfT, message);
        }

        private void Publish<T>(Type topic, T message) where T : IMessage
        {
            Publish(topic.Name, message);
        }

        private void Publish<T>(string topic, T message) where T : IMessage
        {
            if (!_subscribers.ContainsKey(topic)) return;

            foreach (var subscriber in _subscribers[topic]) subscriber.Invoke(message);
        }

        public void Subscribe<T>(IMessageHandler<T> messageHandler) where T : IMessage
        {
            Subscribe(typeof(T), messageHandler);
        }

        public void Subscribe<T>(Type topic, IMessageHandler<T> messageHandler) where T : IMessage
        {
            Subscribe(topic.Name, messageHandler);
        }

        public void Subscribe<T>(string topic, IMessageHandler<T> messageHandler) where T : IMessage
        {
            if (!_subscribers.TryGetValue(topic, out var handlers))
            {
                handlers = new List<Action<IMessage>> { a => messageHandler.Handle((T) a) };
                _subscribers[topic] = handlers;
            }
            else
            {
                var newHandlers = new List<Action<IMessage>>(handlers) { a => messageHandler.Handle((T) a) };
                _subscribers[topic] = newHandlers;
            }
        }
    }
}