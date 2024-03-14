using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpWinLines : HelpPage
{
    [SerializeField]
    TMP_Text noLineTextBox;

    [SerializeField]
    GridLayoutGroup gridLayout;

    [SerializeField]
    GameType winLineGame;
    

    public override void SetData(GameMath gameMath)
    {
        base.SetData(gameMath);
        if (currentMath.GameType == winLineGame)
        {
            gridLayout.gameObject.SetActive(true);
            noLineTextBox.gameObject.SetActive(false);
        }
        else
        {
            gridLayout.gameObject.SetActive(false);
            noLineTextBox.gameObject.SetActive(true);
        }
    }
}
