using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WinLineAnimatorController : MonoBehaviour
{
    [SerializeField]
    WinTextUI winText;

    [SerializeField]
    GameSymbolController symbolController;

    TicketData currentTicket;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        StopAnimations();
    }

    IEnumerator WaitForSymbolCompletion(List<GameSymbol> symbols)
    {
        yield return null;

        while (symbols.Any(x => !x.IsFinished))
        {
            yield return null;
        }
    }

    public void StopAnimations()
    {
        StopAllCoroutines();
        winText.SetText("");
        symbolController.StopAnimations();
    }

    public void SetTicketData(TicketData newTicket)
    {
        currentTicket = newTicket;
        StopAnimations();
    }

    public void DoAnimations()
    {
        StartCoroutine(WinLineLoop());
    }

    string GetWinLineString(WinLineWinAmount winLine)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"{winLine.WinLine.Indices.Count} {winLine.SymbolName} awards {(winLine.WinAmount / 100.0m).ToString("C2")}");

        return sb.ToString();
    }


    IEnumerator WinLineLoop()
    {
        if (currentTicket.WinLines.Count == 0)
        {
            yield break;
        }
        while (true)
        {
            foreach (var line in currentTicket.WinLines)
            {
                var symbols = symbolController.Symbols.GetItemsAtIndices(line.WinLine.Indices);
                symbols.ForEach(x => x.PlayAnimation());
                winText.SetText(GetWinLineString(line));
                yield return StartCoroutine(WaitForSymbolCompletion(symbols));
                symbols.ForEach(x => x.ResetAnimation());
                yield return null;
            }
        }
    }
}
