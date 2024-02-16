using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public abstract class GameMath : ScriptableObject
{
    [SerializeField]
    string gameName;

    public string GameName => gameName;

    public uint NumRows;
    public uint NumColumns;
    public uint TotalSymbols { get => NumRows * NumColumns; }
    public bool AllowFreePlaysInFreePlays;
    public int BaseDenomination;

    public List<int> AllDenominations;

    public List<SymbolData> Symbols;

    protected System.Random random = new System.Random();

    public virtual List<TicketData> GetTickets(int seed, int denomination)
    {
        int multiplier = denomination / BaseDenomination;
        List<TicketData> tickets = new List<TicketData>();

        random = new System.Random(seed);
        var ticket = ScoreTicket(GenerateSymbols(false), multiplier);
        ticket.Denomination = denomination;
        ticket.Seed = seed;
        tickets.Add(ticket);
        int totalFreePlays = ticket.FreePlaysAwarded;

        while (totalFreePlays > 0)
        {
            totalFreePlays--;
            var freePlayTicket = ScoreTicket(GenerateSymbols(true), multiplier);
            freePlayTicket.Denomination = denomination;
            freePlayTicket.Seed = seed;
            totalFreePlays += freePlayTicket.FreePlaysAwarded;
            ticket.WinTotal += freePlayTicket.WinTotal;
            tickets.Add(freePlayTicket);
        }

        return tickets;
    }

    /// <summary>
    /// Creates and Scores a ticket based upon the given symbols. NOTE: Seed should be set after this method
    /// </summary>
    /// <param name="symbolIds"></param>
    /// <returns></returns>
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
                (sym.MaxCount <= 0 || toReturn.GetCountOfItem(GetSymbolIndex(sym)) < sym.MaxCount) &&
                (!isFreePlay || (isFreePlay && sym.IsAllowedInFreePlay)))
                .Select(sym => GetSymbolIndex(sym)).ToList();
        toReturn.Add(allowedSyms.GetRandomItem(indexToWeight, random));
    }

        return toReturn;
    }
}
