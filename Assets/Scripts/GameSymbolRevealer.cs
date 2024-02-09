using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSymbolRevealer : MonoBehaviour
{
    public abstract bool IsRevealed { get; set; }

    public abstract bool IsHidden { get; set; }

    public abstract void RevealSymbol();

    public abstract void HideSymbol();
}
