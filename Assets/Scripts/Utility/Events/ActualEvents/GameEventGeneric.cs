using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventGeneric<T> : ScriptableObject
{
    public T Value;

    List<GameEventListenerGeneric<T>> listeners = new List<GameEventListenerGeneric<T>>();

    public void AddListener(GameEventListenerGeneric<T> newListener)
    {
        foreach (var listener in listeners)
        {
            if (listener == newListener)
            {
                return;
            }
        }
        listeners.Add(newListener);
    }

    public void RemoveListener(GameEventListenerGeneric<T> listener)
    {
        listeners.Remove(listener);
    }

    public void Invoke()
    {
        foreach (var listener in listeners)
        {
            if (listener != null)
            {
                listener.OnEventHit.Invoke(Value);
            }
        }
    }

    public void Invoke(T updatedValue)
    {
        Value = updatedValue;
        foreach (var listener in listeners)
        {
            if (listener != null)
            {
                listener.OnEventHit.Invoke(Value);
            }
        }
    }
}
