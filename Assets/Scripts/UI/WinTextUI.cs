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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SetText("");
    }

    public void SetText(string text)
    {
        textBox.text = text;
    }
}
