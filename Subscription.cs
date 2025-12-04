using System;
using UnityEngine.Assertions;

namespace MessageBus
{
    public class Subscription : IDisposable
    {
        private readonly Action<object> callback;
        private readonly Declaration declaration;

        public Subscription(Action<object> callback, Declaration declaration)
        {
            Assert.IsNotNull(callback);
            this.callback = callback;
            this.declaration = declaration;
            this.declaration.Add(this);
        }

        public void Dispose()
        {
            declaration.Remove(this);
        }

        public void Invoke(object message)
        {
            callback(message);
        }
    }
}