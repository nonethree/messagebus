using System;

namespace MessageBus
{
    public interface IMessageBus
    {
        public void Declare<T>();
        public void Subscribe<T>(Action<T> handler);
        public void Unsubscribe<T>(Action<T> handler);
        public void Send<T>(T message);
    }
}