using System.Collections.Generic;
using UnityEngine.Assertions;

namespace MessageBus
{
    public class Declaration
    {
        public List<Subscription> Subscriptions { get; } = new List<Subscription>();

        public void Add(Subscription subscription)
        {
            Assert.IsFalse(Subscriptions.Contains(subscription));
            Subscriptions.Add(subscription);
        }

        public void Remove(Subscription subscription)
        {
            Subscriptions.Remove(subscription);
        }

        public void Send(object message)
        {
            var subscriptionsCache = new Subscription[Subscriptions.Count];
            Subscriptions.CopyTo(subscriptionsCache);
            for (var i = 0; i < subscriptionsCache.Length; i++)
            {
                if (Subscriptions.Contains(subscriptionsCache[i]))
                {
                    subscriptionsCache[i].Invoke(message);
                }
            }
        }
    }
}