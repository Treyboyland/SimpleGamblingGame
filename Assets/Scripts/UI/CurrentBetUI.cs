using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBetUI : MonoBehaviour
{
    [SerializeField]
    CashTextUI betText;

    // Start is called before the first frame update
    void Start()
    {
        betText.SetCash(GameManager.Manager.CurrentDenomination);
    }
}
