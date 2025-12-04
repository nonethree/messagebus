using System;
using System.Collections.Generic;
using UnityEngine;

namespace MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly Dictionary<Type, Declaration> declarations = new Dictionary<Type, Declaration>();
        private readonly Dictionary<object, Subscription> subscriptions = new Dictionary<object, Subscription>();

        public void Declare<T>()
        {
            if (declarations.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"Message of type {typeof(T).Name} is already declared");
            }
            else
            {
                declarations.Add(typeof(T), new Declaration());
            }
        }

        public void Subscribe<T>(Action<T> callback)
        {
            if (declarations.TryGetValue(typeof(T), out var declaration))
            {
                Action<object> callbackWrapper = o => callback((T) o);

                var subscription = new Subscription(callbackWrapper, declaration);
                subscriptions.Add(callback, subscription);
            }
            else
            {
                Debug.LogWarning($"Message of type {typeof(T).Name} isn't declared");
            }
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            if (subscriptions.TryGetValue(callback, out var subscription))
            {
                subscriptions.Remove(callback);
                subscription.Dispose();
            }
            else
            {
                Debug.LogWarning($"Couldn't find subscription for message {typeof(T).Name}");
            }
        }

        public void Send<T>(T message)
        {
            Send(typeof(T), message);
        }

        private void Send(Type messageType, object message)
        {
            if (declarations.TryGetValue(messageType, out var declaration))
            {
                declaration.Send(message);
            }
            else
            {
                Debug.LogWarning($"Message of type {messageType.Name} isn't declared");
            }
        }
    }
}