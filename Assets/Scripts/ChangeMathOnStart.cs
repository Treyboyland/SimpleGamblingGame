using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMathOnStart : MonoBehaviour
{
    [SerializeField]
    GameMath newMath;

    // Start is called before the first frame update
    void Start()
    {
        if (newMath)
        {
            GameManager.Manager.CurrentMath = newMath;
        }
        else
        {
            Debug.LogWarning("Math given was null. Not setting");
        }
    }
}
