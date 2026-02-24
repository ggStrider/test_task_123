using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Scripts.Core.Reactive
{
    [Serializable]
    public class ReactiveVariable<T>
    {
        public enum SetMode
        {
            TrySetNotSilent = 0,
            TrySetSilent = 1,
            ForceSet = 2
        };
        
        public ReactiveVariable()
        {
            _value = default;
        }

        public ReactiveVariable(T startValue)
        {
            SetValue(startValue);
        }

        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            set => SetValue(value);
        }

        public event Action OnValueChangeNoArgs;
        
        /// <summary>
        /// T1 -> Old Value
        /// T2 -> New Value
        /// </summary>
        public event Action<T, T> OnValueChange;

        public void SetValue(T newValue, SetMode setMode = SetMode.TrySetNotSilent)
        {
            if (setMode != SetMode.ForceSet)
            {
                if (EqualityComparer<T>.Default.Equals(_value, newValue))
                    return;
            }

            if (setMode == SetMode.TrySetSilent)
                return;

            if (setMode is SetMode.TrySetNotSilent or SetMode.ForceSet)
            {
                var oldValue = _value;
                _value = newValue;
                
                OnValueChange?.Invoke(oldValue, newValue);
                OnValueChangeNoArgs?.Invoke();
            }
        }
    }
}