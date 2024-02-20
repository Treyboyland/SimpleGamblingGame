using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElapsedTime : MonoBehaviour
{
    [SerializeField]
    TMP_Text textBox;

    public void UpdateElapsed(float elapsed)
    {
        TimeSpan ts = new TimeSpan(0, 0, 0, (int)elapsed, (int)((elapsed % 1) * 100));
        textBox.text = ts.ToString();
    }
}
