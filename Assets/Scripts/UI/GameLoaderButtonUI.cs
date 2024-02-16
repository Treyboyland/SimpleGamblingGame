using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLoaderButtonUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text textBox;

    [SerializeField]
    GameMath gameMath;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = gameMath.GameName;
    }

    public void SetManagerMath()
    {
        GameManager.Manager.CurrentMath = gameMath;
    }
}
