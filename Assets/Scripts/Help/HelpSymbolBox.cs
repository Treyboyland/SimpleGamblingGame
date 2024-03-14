using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpSymbolBox : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Color activeColor;

    [SerializeField]
    Color inactiveColor;

    public bool LightUp
    {
        set
        {
            image.color = value ? activeColor : inactiveColor;
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        LightUp = false;
    }
}
