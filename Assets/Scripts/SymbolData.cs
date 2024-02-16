using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "Symbol-", menuName = "Math/Symbol", order = 1)]
public class SymbolData : ScriptableObject
{
    /// <summary>
    /// This should be unique between the symbols
    /// </summary>
    [Tooltip("Make sure name is unique")]
    public string SymbolName;
    [Tooltip("True if this symbol is allowed in free plays")]
    public bool IsAllowedInFreePlay = true;
    public Sprite SymbolSprite;
    public Color SymbolColor = Color.white;
    public RuntimeAnimatorController SymbolAnimations;
    public List<WinAmountCount> WinAmounts;

    /// <summary>
    /// True if symbol replaces others
    /// </summary>
    [Tooltip("If wild, this symbol can replace another in a line, or add to a scatter. Should not itself have win amounts")]
    public bool IsWild;
    /// <summary>
    /// Max number of this symbol on a ticket, if non-zero
    /// </summary>
    [Tooltip("If greater than zero, will limit count of this symbol on the ticket")]
    public int MaxCount;

    /// <summary>
    /// Weight of this symbol compared to other symbols. Should be non-zero
    /// </summary>
    [Tooltip("Make sure this is non-zero")]
    public int Weight;

    public bool HasWinCount(int count, out WinAmountCount winCount)
    {
        var maxCount = WinAmounts.Max(x => x.Count);
        var maxWin = WinAmounts.Where(x => x.Count == maxCount).First();
        if (count >= maxWin.Count)
        {
            //Not sure if this is good for scatters...(e.g. 6 on ticket -> 5 vs 3 and 3)
            winCount = maxWin;
        }
        else
        {
            winCount = WinAmounts.Where(x => x.Count == count).FirstOrDefault();
        }

        return winCount != default(WinAmountCount);
    }
}
