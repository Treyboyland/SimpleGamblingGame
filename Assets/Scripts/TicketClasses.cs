
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;


[Serializable]
public class WinAmountCount
{
    /// <summary>
    /// Win for this line
    /// </summary>
    public int WinAmount;
    /// <summary>
    /// Count required for win
    /// </summary>
    public uint Count;
    /// <summary>
    /// True if free play symbol, which implies that the win amount
    /// is a free play count
    /// </summary>
    public bool IsFreePlay;
    /// <summary>
    /// True if this should award a bonus feature
    /// </summary>
    public bool IsBonusFeature;
}

[Serializable]
public class WinLine
{
    public List<int> Indices;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < Indices.Count; i++)
        {
            sb.Append(Indices[i] + (i != Indices.Count - 1 ? "," : ""));
        }
        return sb.ToString();
    }
}

public class WinLineWinAmount
{
    public WinLine WinLine;
    public int WinAmount;
    public string SymbolName;

    public bool HasFreePlays;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"Line {WinLine}, Symbol {SymbolName}, Free Play: {HasFreePlays}, WinAmount: {WinAmount}");

        return sb.ToString();
    }
}

[Serializable]
public class TicketData
{
    public int Seed;
    public int Denomination;
    public int WinTotal;
    public List<WinLineWinAmount> WinLines = new List<WinLineWinAmount>();
    public List<int> SymbolIds;
    public bool HasFreePlays;
    public int FreePlaysAwarded;

    public override string ToString()
    {
        return ToString(5);
    }

    public string ToString(int numColumns)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < SymbolIds.Count; i++)
        {
            sb.Append(SymbolIds[i].ToString("00"));
            sb.Append(i != 0 && (i + 1) % numColumns == 0 ? "\r\n" : " ");
        }
        sb.Append("\r\n");
        sb.AppendLine($"Seed: {Seed}");
        sb.AppendLine($"Win Amount: {WinTotal}");
        sb.AppendLine($"Free Plays: {HasFreePlays}");
        sb.AppendLine($"Free Plays Awarded: {FreePlaysAwarded}");

        for (int i = 0; i < WinLines.Count; i++)
        {
            sb.Append(WinLines[i] + (i == WinLines.Count - 1 ? "" : "|"));
        }

        return sb.ToString();
    }
}