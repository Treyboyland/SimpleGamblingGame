using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Shows the description of the win line
/// </summary>
public class WinTextUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text textBox;

    public void SetText(string text)
    {
        textBox.text = text;
    }
}
