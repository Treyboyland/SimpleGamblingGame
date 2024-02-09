using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSymbolRevealerController : MonoBehaviour
{
    [SerializeField]
    GameEvent onAllRevealed;

    [SerializeField]
    List<GameSymbolRevealer> symbolRevealers;

    public bool AreAllRevealed()
    {
        return symbolRevealers.All(x => x.IsRevealed);
    }

    public bool AreAllHidden()
    {
        return symbolRevealers.All(x => !x.IsRevealed);
    }

    public void RevealSymbols()
    {
        foreach (var symbol in symbolRevealers)
        {
            symbol.RevealSymbol();
        }
    }

    public void HideSymbols()
    {
        foreach (var symbol in symbolRevealers)
        {
            symbol.HideSymbol();
        }

        StartCoroutine(WaitForReveal());
    }

    IEnumerator WaitForReveal()
    {
        yield return null;
        while (!AreAllRevealed())
        {
            yield return null;
        }

        onAllRevealed.Invoke();
    }
}
