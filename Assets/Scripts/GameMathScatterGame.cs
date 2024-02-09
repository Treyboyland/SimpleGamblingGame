using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MathScatter-", menuName = "Math/GameMath/Scatter", order = 0)]
public class GameMathScatterGame : GameMath
{
    public int MaxCount;

    public override List<TicketData> GetTickets(int seed, int denomination)
    {
        int multiplier = denomination / BaseDenomination;
        List<TicketData> tickets = new List<TicketData>();

        random = new System.Random(seed);
        var ticket = ScoreTicket(GenerateSymbols(false), multiplier);
        ticket.Denomination = denomination;
        tickets.Add(ticket);
        int totalFreePlays = ticket.FreePlaysAwarded;

        while (totalFreePlays > 0)
        {
            totalFreePlays--;
            var freePlayTicket = ScoreTicket(GenerateSymbols(true), multiplier);
            freePlayTicket.Denomination = denomination;
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
    public override TicketData ScoreTicket(List<int> symbolIds, int multiplier)
    {
        TicketData ticket = new TicketData();
        ticket.SymbolIds = symbolIds;

        var allSymbolCounts = GetSymbolCounts(symbolIds);

        foreach (var symbol in Symbols)
        {
            var symId = GetSymbolIndex(symbol);
            var symCount = allSymbolCounts.GetCount(symId);
            if (symbol.HasWinCount(symCount, out WinAmountCount winCountSym))
            {
                if (winCountSym.IsFreePlay)
                {
                    //TODO: Add Free Play logic
                    ticket.HasFreePlays = true;
                    ticket.FreePlaysAwarded += winCountSym.WinAmount;
                }
                else
                {
                    ticket.WinTotal += (int)(winCountSym.WinAmount * multiplier);
                    List<int> indices = symbolIds.GetIndicesOfItem(symId);
                    ticket.WinLines.Add(new WinLineWinAmount()
                    {
                        WinAmount = (int)(winCountSym.WinAmount * multiplier),
                        WinLine = new WinLine() { Indices = indices },
                        SymbolName = symbol.SymbolName
                    });
                }
            }
        }

        return ticket;
    }

    protected override List<int> GenerateSymbols(bool isFreePlay)
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
            //Limiting occurence of symbols on ticket. NOTE: Issue with SYM*MAX_COUNT < DIMENSIONS
            List<int> possible = Symbols.Where(sym =>
                (GetSymbolCount(sym, toReturn) < MaxCount || MaxCount < 1)
                && (!isFreePlay || (isFreePlay && sym.IsAllowedInFreePlay)))
                .Select(sym => GetSymbolIndex(sym)).ToList();
            toReturn.Add(possible.GetRandomItem(indexToWeight, random));
        }

        return toReturn;
    }
}