using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpGameDescription : HelpPage
{
    [SerializeField]
    TMP_Text titleText;

    [SerializeField]
    TMP_Text description;

    public override void SetData(GameMath gameMath)
    {
        base.SetData(gameMath);
        titleText.text = currentMath.GameName;
        description.text = currentMath.GameDescription;
    }
}
