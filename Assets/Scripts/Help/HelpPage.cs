using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HelpPage : MonoBehaviour
{
    protected GameMath currentMath;

    public virtual void SetData(GameMath gameMath)
    {
        currentMath = gameMath;
    }
}
