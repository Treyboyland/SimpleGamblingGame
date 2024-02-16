using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStringParser : MonoBehaviour
{
    [SerializeField]
    GameEventInt eventInt;

    public void SetValue(string intValue)
    {
        eventInt.Value = int.Parse(intValue);
    }
}
