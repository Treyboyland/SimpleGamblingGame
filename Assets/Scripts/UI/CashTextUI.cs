using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashTextUI : MonoBehaviour
{
    [SerializeField]
    int leadingZeroes;

    [SerializeField]
    TMP_Text textBox;

    [SerializeField]
    bool setInitialValue;

    [SerializeField]
    int initialValue;

    // Start is called before the first frame update
    void Start()
    {
        if (setInitialValue)
        {
            SetCash(initialValue);
        }
    }

    public void SetCash(int cash)
    {
        textBox.text = (cash / 100.0m).ToString("C2");
    }
}
