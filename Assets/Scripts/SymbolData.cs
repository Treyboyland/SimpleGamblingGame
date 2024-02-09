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
    /// Weight of this symbol compared to other symbols. Should be non-zero
    /// </summary>
    [Tooltip("Make sure this is non-zero")]
    public int Weight;

    public bool HasWinCount(int count, out WinAmountCount winCount)
    {
        winCount = WinAmounts.Where(x => x.Count == count).FirstOrDefault();
        return winCount != default(WinAmountCount);
    }
}
