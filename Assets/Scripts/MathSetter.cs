using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathSetter : MonoBehaviour
{
    public void SetMath(GameMath newMath)
    {
        GameManager.Manager.CurrentMath = newMath;
    }
}
