using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpPayTable : HelpPage
{
    [SerializeField]
    List<HelpPayTableSlot> payTableSlots;

    [SerializeField]
    TMP_Text textBox;

    int initialPageIndex = 0;

    public override void SetData(GameMath gameMath)
    {
        base.SetData(gameMath);
    }

    void SetPaysForPage()
    {
        textBox.text = $"{GameManager.Manager.CurrentDenomination.ToCash()} Paytable";
        int start = payTableSlots.Count * initialPageIndex;

        for (int i = 0; i < payTableSlots.Count; i++)
        {
            int payDataIndex = start + i;
            if (payDataIndex >= currentMath.Symbols.Count)
            {
                payTableSlots[i].gameObject.SetActive(false);
            }
            else if (currentMath.Symbols[payDataIndex].IsWild || currentMath.Symbols[payDataIndex].WinAmounts.Count == 0)
            {
                payTableSlots[i].gameObject.SetActive(false);
            }
            else
            {
                payTableSlots[i].gameObject.SetActive(true);
                payTableSlots[i].SetData(currentMath.Symbols[payDataIndex], currentMath.GetMultiplier(GameManager.Manager.CurrentDenomination));
            }
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        initialPageIndex = 0;
        if (gameObject.activeInHierarchy && currentMath != null)
        {
            SetPaysForPage();
        }
    }

    public void NextPage()
    {
        initialPageIndex++;
        if (initialPageIndex * payTableSlots.Count >= currentMath.Symbols.Count)
        {
            initialPageIndex = 0;
        }
        SetPaysForPage();
    }

    public void PreviousPage()
    {
        initialPageIndex--;
        if (initialPageIndex < 0)
        {
            if (currentMath.Symbols.Count % payTableSlots.Count == 0)
            {
                initialPageIndex = currentMath.Symbols.Count / payTableSlots.Count - 1;
            }
            else
            {
                initialPageIndex = currentMath.Symbols.Count / payTableSlots.Count;
            }
        }

        SetPaysForPage();
    }
}
