using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSymbolController : MonoBehaviour
{


    [SerializeField]
    List<GameSymbol> symbols;

    public List<GameSymbol> Symbols { get => symbols; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSymbols(TicketData ticketData)
    {
        if (ticketData.SymbolIds.Count != symbols.Count)
        {
            Debug.LogWarning("Need more symbols");
        }

        for (int i = 0; i < ticketData.SymbolIds.Count; i++)
        {
            symbols[i].SetSymbol(GameManager.Manager.CurrentMath.Symbols[ticketData.SymbolIds[i]]);
        }
    }

    public void StopAnimations()
    {
        foreach (var symbol in symbols)
        {
            symbol.ResetAnimation();
        }
    }
}
