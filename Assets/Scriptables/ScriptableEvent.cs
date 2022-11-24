using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scriptables
{
    public abstract class ScriptableEvent<T> : ScriptableObject
    {
        private List<ScriptableEventListener<T>> Listeners = new List<ScriptableEventListener<T>>();
        public void RegisterListener(ScriptableEventListener<T> listener)
        {
            if (!Listeners.Contains(listener))
            {
                Listeners.Add(listener);
            }
        }
        public void UnregisterListener(ScriptableEventListener<T> listener)
        {
            if (Listeners.Contains(listener))
            {
                Listeners.Remove(listener);
            }
        }
        public int Raise(T value)
        {
            foreach (ScriptableEventListener<T> listener in Listeners)
            {
                listener.OnEventRaised(value);
            }
            return Listeners.Count;
        }
    }
}
