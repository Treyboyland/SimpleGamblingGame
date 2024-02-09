using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public abstract class GameMath : ScriptableObject
{
    public uint NumRows;
    public uint NumColumns;
    public bool AllowFreePlaysInFreePlays;
    public int BaseDenomination;

    public List<int> AllDenominations;

    public List<SymbolData> Symbols;

    protected System.Random random = new System.Random();

    public abstract List<TicketData> GetTickets(int seed, int denomination);

    public abstract TicketData ScoreTicket(List<int> symbolIds, int multiplier);

    public int GetSymbolIndex(SymbolData symbol)
    {
        for (int i = 0; i < Symbols.Count; i++)
        {
            if (Symbols[i].SymbolName == symbol.SymbolName)
            {
                return i;
            }
        }

        return -1;
    }

    public int GetSymbolCount(SymbolData symbol, List<int> syms)
    {
        int index = GetSymbolIndex(symbol);

        return syms.GetCountOfItem(index);
    }

    public Dictionary<int, int> GetSymbolCounts(List<int> syms)
    {
        Dictionary<int, int> toReturn = syms.GetCounts();
        List<int> ids = new List<int>(Symbols.Count);

        for (int i = 0; i < Symbols.Count; i++)
        {
            ids.Add(i);
        }

        foreach (var id in ids)
        {
            if (!toReturn.ContainsKey(id))
            {
                toReturn.Add(id, 0);
            }
        }

        return toReturn;
    }

    /// <summary>
    /// Returns a list of indicies in the symbols list
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns>
    protected virtual List<int> GenerateSymbols(bool isFreePlay)
    {
        uint numSymbols = NumRows * NumColumns;

        List<int> symbolIds = new List<int>(Symbols.Count);
        List<int> toReturn = new List<int>(Symbols.Count);
        for (int i = 0; i < Symbols.Count; i++)
        {
            symbolIds.Add(i);
        }

        int indexToWeight(int x) => Symbols[x].Weight;

        for (uint i = 0; i < numSymbols; i++)
        {
            var allowedSyms = Symbols.Where(sym =>
                !isFreePlay || (isFreePlay && sym.IsAllowedInFreePlay))
                .Select(sym => GetSymbolIndex(sym)).ToList();
            toReturn.Add(allowedSyms.GetRandomItem(indexToWeight, random));
        }

        return toReturn;
    }
}
