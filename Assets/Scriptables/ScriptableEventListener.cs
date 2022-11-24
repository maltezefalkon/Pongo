using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scriptables
{
    public class ScriptableEventListener<T> : MonoBehaviour
    {
        public ScriptableEvent<T> Event;
        
        [SerializeField]
        private UnityEvent<T> Response;

        public void OnEnable()
        {
            Event?.RegisterListener(this);
        }

        public void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(T value)
        {
            Response?.Invoke(value);
        }
    }
}
