using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTitleUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = GameManager.Manager.CurrentMath.GameName;
    }
}
