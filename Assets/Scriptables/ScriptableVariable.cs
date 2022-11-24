using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scriptables
{
    public abstract class ScriptableVariable<T> : ScriptableObject, ISerializationCallbackReceiver where T : struct
    {
        public class ValueChangeEventArgs
        {
            public ValueChangeEventArgs(T oldValue, T newValue)
            {
                OldValue = oldValue;
                NewValue = newValue;
            }

            public T OldValue
            {
                get;
            }

            public T NewValue 
            { 
                get; 
            }
        }

        public T InitialValue;

        [NonSerialized]
        private T _runtimeValue;

        public event EventHandler<ValueChangeEventArgs> BeforeValueChanged;
        public event EventHandler<ValueChangeEventArgs> AfterValueChanged;

        public T RuntimeValue
        {
            get
            {
                return _runtimeValue;
            }
            set
            {
                if (!_runtimeValue.Equals(value))
                {
                    T oldValue = _runtimeValue;
                    OnBeforeValueChanged(oldValue, value);
                    _runtimeValue = value;
                    OnAfterValueChanged(oldValue, value);
                }
            }
        }

        private void OnAfterValueChanged(T oldValue, T newValue)
        {
            AfterValueChanged?.Invoke(this, new ValueChangeEventArgs(oldValue, newValue));
        }

        private void OnBeforeValueChanged(T oldValue, T newValue)
        {
            BeforeValueChanged?.Invoke(this, new ValueChangeEventArgs(oldValue, newValue));
        }

        public virtual void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public virtual void OnBeforeSerialize() { }
    }
}
