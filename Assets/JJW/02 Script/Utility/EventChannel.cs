using System;
using UnityEngine;

namespace JJW._02_Script.Utility
{
    public abstract class EventChannel<T> : ScriptableObject {
        public event Action<T> OnEvent;
        public void Raise(T item) => OnEvent?.Invoke(item);
    }
}