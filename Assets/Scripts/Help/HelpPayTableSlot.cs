using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HelpPayTableSlot : MonoBehaviour
{
    [SerializeField]
    Image symbolImage;

    [SerializeField]
    TMP_Text payText;

    public void SetData(SymbolData symbolData, int multiplier)
    {
        symbolImage.sprite = symbolData.SymbolSprite;
        symbolImage.color = symbolData.SymbolColor;
        StringBuilder sb = new StringBuilder();

        var orderedWins = symbolData.WinAmounts.OrderBy(x => x.Count);

        foreach (var win in orderedWins)
        {
            sb.AppendLine($"{win.Count}x {(win.WinAmount * multiplier).ToCash()}");
        }
        payText.text = sb.ToString();
    }
}
