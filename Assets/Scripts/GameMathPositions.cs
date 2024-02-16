using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MathPositions-", menuName = "Math/GameMath/Positions", order = 1)]
public class GameMathPositions : GameMath
{
    [SerializeField]
    bool mustStartOnLeft;

    [SerializeField]
    List<WinLine> winLines;


    public override TicketData ScoreTicket(List<int> symbolIds, int multiplier)
    {
        TicketData ticket = new TicketData();
        ticket.SymbolIds = symbolIds;

        var wildIds = Symbols.Where(x => x.IsWild).Select(x => GetSymbolIndex(x)).ToList();

        foreach (var line in winLines)
        {
            var lineSymbols = ticket.SymbolIds.GetItemsAtIndices(line.Indices);

            var counts = lineSymbols.GetMaxConsecutiveIndices(wildIds, mustStartOnLeft);

            foreach (var count in counts)
            {
                var symbolId = count.Key;
                var symbol = Symbols[symbolId];
                if (symbol.HasWinCount(count.Value.Count, out WinAmountCount winCount))
                {
                    if (winCount.IsFreePlay)
                    {
                        //TODO: Add Free Play logic
                        ticket.HasFreePlays = true;
                        ticket.FreePlaysAwarded += winCount.WinAmount;
                    }
                    else
                    {
                        ticket.WinTotal += (int)(winCount.WinAmount * multiplier);
                        List<int> indices = line.Indices.GetItemsAtIndices(count.Value);
                        ticket.WinLines.Add(new WinLineWinAmount()
                        {
                            WinAmount = (int)(winCount.WinAmount * multiplier),
                            WinLine = new WinLine() { Indices = indices },
                            SymbolName = symbol.SymbolName
                        });
                    }
                }
            }

        }

        return ticket;
    }
}
