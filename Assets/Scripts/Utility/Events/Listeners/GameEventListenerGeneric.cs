using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerGeneric<T> : MonoBehaviour
{
    [SerializeField]
    GameEventGeneric<T> gameEvent;

    public UnityEvent<T> OnEventHit = new UnityEvent<T>();

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }
}
