using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent-On", menuName = "Utility/Game Event/No Params", order = 0)]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners = new List<GameEventListener>();

    public void AddListener(GameEventListener newListener)
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

    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void Invoke()
    {
        foreach(var listener in listeners)
        {
            if(listener != null)
            {
                listener.OnEventHit.Invoke();
            }
        }
    }
}
