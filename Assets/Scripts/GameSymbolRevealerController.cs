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

    [SerializeField]
    bool delayedReveal;

    [SerializeField]
    float secondsBetweenReveals;

    bool isRevealing = false;

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
        if (!delayedReveal)
        {
            foreach (var symbol in symbolRevealers)
            {
                symbol.RevealSymbol();
            }
        }
        else if (!isRevealing)
        {
            StartCoroutine(DelayedReveal());
        }

    }


    IEnumerator DelayedReveal()
    {
        isRevealing = true;
        foreach (var symbol in symbolRevealers)
        {
            symbol.RevealSymbol();
            yield return new WaitForSeconds(secondsBetweenReveals);
        }
        isRevealing = false;
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
